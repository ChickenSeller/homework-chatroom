using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace ChatroomClient
{
    class Client
    {
        public IPEndPoint clientip;
        public IPEndPoint serverip;
        public Socket socket;
        public User users;
        public Chatroom chatrooms;
        public UserNode myself;
        public Chatroom my_chatrooms;
        public FormLogin formLogin;
        public FormMain formMain;
        public ArrayList formChatrooms;

        public Client()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int port = rand.Next(5000, 65535);
            this.clientip = new IPEndPoint(IPAddress.Any, port);
            this.serverip = new IPEndPoint(IPAddress.Any, port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.chatrooms = new Chatroom();
            this.my_chatrooms = new Chatroom();
            this.users = new User();
            this.formChatrooms = new ArrayList();
            this.socket.Bind(this.clientip);
            this.formLogin = new FormLogin();
            Thread thread = new Thread(new ThreadStart(this.ReceiveMsg));
            thread.Start();
            
        }

        public void HandleLoginResponse(LoginResponse package,EndPoint point)
        {
            if (package.data.status != 0)
            {
                Program.ShowFormMessageBox temp = SendFormMessage;
                object obj = this.formLogin;
                temp("登录失败", ref obj);
            }
            else
            {
                this.myself = new UserNode(package.data.user_id, package.data.user_name);
                this.formMain = new FormMain(this.myself,this.serverip);
                Thread thread = new Thread(new ThreadStart(this.openFormMain));
                thread.Start();
                this.formLogin.Close();
            }
        }

        public void HandleGetChatroomListResponse(GetChatroomListResponse package,EndPoint point)
        {
            if (package.data.status == 0)
            {
                this.chatrooms.chatrooms = package.data.chatrooms;
                this.formMain.RefreshChatroomList();
            }
        }

        public void HandleJoinChatroomResponse(JoinChatroomResponse package, EndPoint point)
        {
            if (package.data.status != 0)
            {
                return;
            }
                int index = -1;
            int num = Program.client.chatrooms.chatrooms.Count;
            for(int i=0;i<num;i++)
            {
                if (((ChatroomNode)(Program.client.chatrooms.chatrooms[i])).ChatroomID == package.data.chatroom_id)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)
            {
                return;
            }
            ChatroomNode tempNode = ((ChatroomNode)(Program.client.chatrooms.chatrooms[index]));
            tempNode.ChatroomMembers = package.data.members;
            Program.client.chatrooms.chatrooms[index] = tempNode;
            RefreshChatroomForms();
        }
        public void openFormMain()
        {
            Application.Run(this.formMain);
        }
        public void openFormChatroom(object par)
        {
            int i = GetFormChatroom((ChatroomNode)par);
            FormChatroomNode tempNode = (FormChatroomNode)(this.formChatrooms[i]);
            tempNode.is_running = true;
            this.formChatrooms[i] = tempNode;
            Application.Run(((FormChatroomNode)(this.formChatrooms[i])).form);
        }
        public void DoLogin(string UserName)
        {
            Login loginPackage = new Login(UserName, this.clientip.Port);
            this.SendMsg(loginPackage, this.serverip);
        }
        public int GetFormChatroom(ChatroomNode node)
        {
            int count = this.formChatrooms.Count;
            for(int i=0; i<count; i++)
            {
                if (((FormChatroomNode)(this.formChatrooms[i])).chatroom_id == node.ChatroomID)
                {
                    return i;
                }
            }
            return -1;
        }
        public void AddFormChatroom(ChatroomNode node)
        {
            int count = this.formChatrooms.Count;
            for (int i = 0; i < count; i++)
            {
                if (((FormChatroomNode)(this.formChatrooms[i])).chatroom_id == node.ChatroomID)
                {
                    return;
                }
            }
            FormChatroomNode tempNode = new FormChatroomNode(node);
            this.formChatrooms.Add(tempNode);
        }

        public void ReceiveMsg()
        {
            while (true)
            {
                string ReceivedMessage = "";
                EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int length = 0;
                    try
                    {
                        length = this.socket.ReceiveFrom(buffer, ref remotePoint);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    string tempStr = Encoding.UTF8.GetString(buffer, 0, length);
                    ReceivedMessage += tempStr;
                    if (length < 1024)
                    {
                        break;
                    }

                }
                DataPackage dataPackage;
                try
                {
                    dataPackage = JsonConvert.DeserializeObject<DataPackage>(ReceivedMessage);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception Catched: " + e.Message);
                    return;
                }
                if (dataPackage == null)
                {
                    return;
                }
                switch (dataPackage.message_code)
                {
                    case (int)DataPackage.MESSAGE_CODE.LOGIN_RESPONSE:
                        LoginResponse package1 = JsonConvert.DeserializeObject<LoginResponse>(ReceivedMessage);
                        HandleLoginResponse(package1, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.GET_CHATROOM_LIST_RESPONSE:
                        GetChatroomListResponse package2 = JsonConvert.DeserializeObject<GetChatroomListResponse>(ReceivedMessage);
                        JsonDeserilze<ChatroomNode>(ref package2.data.chatrooms);
                        HandleGetChatroomListResponse(package2, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.JOIN_CHATROOM_RESPONSE:
                        JoinChatroomResponse package3 = JsonConvert.DeserializeObject<JoinChatroomResponse>(ReceivedMessage);
                        JsonDeserilze<UserNode>(ref package3.data.members);
                        HandleJoinChatroomResponse(package3, remotePoint);
                        ReceivedMessage = "";
                        break;
                }
            }
            

        }

        public void SendMsg(object response, EndPoint point)
        {
            string TextResponse = JsonConvert.SerializeObject(response);
            this.socket.SendTo(Encoding.UTF8.GetBytes(TextResponse), point);
        }

        public void BroadcastMsg(object response,ArrayList users)
        {

        }

        public void JsonDeserilze<T>(ref ArrayList list)
        {
            ArrayList tempArray = new ArrayList();
            foreach(JObject item in list)
            {
                tempArray.Add(item.ToObject<T>());
            }
            list.Clear();
            list = tempArray;
        }

        public static void SendFormMessage(string msg,ref object form)
        {
            Type x = form.GetType();
            MessageBox.Show((Form)form, msg);
        }

        public static void ShowForm(ref object form)
        {
            ((Form)form).Show();
        }
        public void InitServerInfo(string ip,int port)
        {
            this.serverip.Address = IPAddress.Parse(ip);
            this.serverip.Port = port;
        }

        public void RefreshChatroomForms()
        {
            foreach(ChatroomNode tempNode in Program.client.chatrooms.chatrooms)
            {
                int roomID = Program.client.GetFormChatroom(tempNode);
                if (roomID == -1)
                {
                    continue;
                }
                if (((FormChatroomNode)(this.formChatrooms[roomID])).is_running == false)
                {
                    continue;
                }
                ((FormChatroomNode)(this.formChatrooms[roomID])).form.RefreshChatroomInfo();
            }
        }

    }

    class FormChatroomNode
    {
        public int chatroom_id;
        public FormChatroom form;
        public bool is_running;
        public FormChatroomNode(ChatroomNode chatroom)
        {
            this.chatroom_id = chatroom.ChatroomID;
            this.form = new FormChatroom(chatroom);
            this.is_running = false;
        }
    }

}
