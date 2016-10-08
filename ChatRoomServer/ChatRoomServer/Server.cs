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
            const int SIP_UDP_CONNRESET = -1744830452;
            this.socket.IOControl(SIP_UDP_CONNRESET, new byte[] { 0, 0, 0, 0 }, null);
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
                Log(e.Message);
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
                        ServerHandler.HandleLogin(package1, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.GET_CHATROOM_LIST:
                        GetChatroomList package2 = JsonConvert.DeserializeObject<GetChatroomList>(ReceivedMessage);
                        ServerHandler.HandleGetChatroomList(package2, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.JOIN_CHATROOM:
                        JoinChatroom package3 = JsonConvert.DeserializeObject<JoinChatroom>(ReceivedMessage);
                        ServerHandler.HandleJoinChatroom(package3, remotePoint);
                        ReceivedMessage = "";
                        break;
                    case (int)DataPackage.MESSAGE_CODE.CHAT_MESSAGE:
                        ChatMessage package4 = JsonConvert.DeserializeObject<ChatMessage>(ReceivedMessage);
                        ServerHandler.HandleChatMessage(package4, remotePoint);
                        ReceivedMessage = "";
                        break;


                }
                remotePoint = null;
            }
        }

        public void SendMsg(object response,EndPoint point)
        {
            string TextResponse = JsonConvert.SerializeObject(response);
            this.socket.SendTo(Encoding.UTF8.GetBytes(TextResponse), point);
        }

        public void BroadcastMsg(object response,ArrayList users)
        {
            EndPoint point = new IPEndPoint(IPAddress.Any, 0);
            string TextResponse = JsonConvert.SerializeObject(response);
            foreach (UserNode temp in users)
            {
                point =new IPEndPoint(IPAddress.Parse(temp.UserIP), temp.UserPort);
                this.socket.SendTo(Encoding.UTF8.GetBytes(TextResponse), point);
            }
        }
        public void Log(string msg)
        {
            Program.frm.Log(msg);
        }

    }
}
