using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Collections;



namespace ChatRoomServer
{
    class Server
    {
        public IPEndPoint serverip;
        public Socket socket;
        public User users;
        public Chatroom chatrooms;
        public bool RunningFlag;
        Thread WorkerThread;
        public Server(int port)
        {
            this.serverip = new IPEndPoint(IPAddress.Any, port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.users = new User();
            this.chatrooms = new Chatroom();
            this.RunningFlag = false;
        }

        public int StartServer()
        {
            if (this.RunningFlag)
            {
                return -1;
            }
            try
            {
                this.socket.Bind(serverip);
            }
            catch(Exception e)
            {
                return 1;
            }
            
            this.WorkerThread = new Thread(new ThreadStart(this.ReceiveMsg));
            this.WorkerThread.IsBackground = true;
            this.WorkerThread.Start();
            this.RunningFlag = true;
            Log("服务启动成功！\t"+this.serverip.ToString());
            return 0;
        }

        public int StopServer()
        {
            if (!this.RunningFlag)
            {
                return -1;
            }
            this.WorkerThread.Abort();
            this.socket.Close();
            Log("服务已停止");
            return 0;
        }

        public void HandleLogin(Login package,EndPoint point)
        {
            UserNode user = this.users.AddUser(((IPEndPoint)point).Address.ToString(), package.data.user_port, package.data.user_name);
            LoginResponse response = new LoginResponse((int)DataPackage.STATUS_CODE.OK, user.UserID, user.UserName);
            Log("用户登录:\t" + user.UserIP+":"+user.UserPort.ToString()+"\tID:"+user.UserID.ToString()+"\tName:"+user.UserName);
            this.SendMsg(response, point);
        }

        public void HandleGetChatroomList(GetChatroomList package, EndPoint point)
        {
            GetChatroomListResponse response = new GetChatroomListResponse((int)DataPackage.STATUS_CODE.OK, this.chatrooms.chatrooms);
            UserNode user = this.users.GetUserByID(package.data.user_id);
            Log("获取聊天室列表:\t" + user.UserIP + ":" + user.UserPort.ToString() + "\tID:" + user.UserID.ToString() + "\tName:" + user.UserName);
            this.SendMsg(response, point);

        }

        public void HandleJoinChatroom(JoinChatroom package, EndPoint point)
        {
            ChatroomNode chatroom = this.chatrooms.GetChatRoomByID(package.data.chatroom_id);
            int index = this.chatrooms.chatrooms.IndexOf(chatroom);
            UserNode user = this.users.GetUserByID(package.data.user_id);
            chatroom.AddMember(user);
            this.chatrooms.chatrooms.RemoveAt(index);
            this.chatrooms.chatrooms.Insert(index, chatroom);
            JoinChatroomResponse response = new JoinChatroomResponse((int)DataPackage.STATUS_CODE.OK, chatroom.ChatroomID, chatroom.ChatroomMembers);
            Log("加入聊天室:\tUser ID:"+ user.UserIP + ":" + user.UserPort.ToString() + "\tChatroom ID:" + chatroom.ChatroomID.ToString());
            this.SendMsg(response, point);
        }

        public void HandleExitChatroom(ExitChatroom package, EndPoint point)
        {
            ChatroomNode chatroom = this.chatrooms.GetChatRoomByID(package.data.chatroom_id);
            int index = this.chatrooms.chatrooms.IndexOf(chatroom);
            UserNode user = this.users.GetUserByID(package.data.user_id);
            chatroom.DelMember(user);
            this.chatrooms.chatrooms.RemoveAt(index);
            this.chatrooms.chatrooms.Insert(index, chatroom);
            ExitChatroomResponse response = new ExitChatroomResponse((int)DataPackage.STATUS_CODE.OK, chatroom.ChatroomID);
            this.SendMsg(response, point);
        }


        public void HandleChatMessage(ChatMessage package, EndPoint point)
        {

        }

        public void ReceiveMsg()
        {
            while (this.RunningFlag)
            {
                string ReceivedMessage = "";
                EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int length = this.socket.ReceiveFrom(buffer, ref remotePoint);
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
                switch (dataPackage.message_code)
                {
                    case (int)DataPackage.MESSAGE_CODE.LOGIN:
                        Login package1 = JsonConvert.DeserializeObject<Login>(ReceivedMessage);
                        HandleLogin(package1, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.GET_CHATROOM_LIST:
                        GetChatroomList package2 = JsonConvert.DeserializeObject<GetChatroomList>(ReceivedMessage);
                        HandleGetChatroomList(package2, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.JOIN_CHATROOM:
                        JoinChatroom package3 = JsonConvert.DeserializeObject<JoinChatroom>(ReceivedMessage);
                        HandleJoinChatroom(package3, remotePoint);
                        ReceivedMessage = "";
                        break;

                }
            }
            

        }

        public void SendMsg(object response,EndPoint point)
        {
            string TextResponse = JsonConvert.SerializeObject(response);
            this.socket.SendTo(Encoding.UTF8.GetBytes(TextResponse), point);
        }
        public void Log(string msg)
        {
            Program.frm.Log(msg);
        }

    }
}
