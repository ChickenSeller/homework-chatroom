using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ChatRoomServer
{
    public class DataPackage
    {
        public int message_code;
        public object data;
        public enum MESSAGE_CODE :int
        {   LOGIN = 100,LOGIN_RESPONSE = 101,
            GET_CHATROOM_LIST = 110,GET_CHATROOM_LIST_RESPONSE = 111,
            JOIN_CHATROOM = 120,JOIN_CHATROOM_RESPONSE = 121,
            EXIT_CHATROOM = 130,EXIT_CHATROOM_RESPONSE = 131,
            CHAT_MESSAGE = 140,PUSH_MESSAGE = 145,
            LOGOUT = 150,LOGOUT_RESPONSE = 151,
            UPDATE_USERS = 200,
            UPDATE_CHATROOMS = 201,
            SERVER_DOWN = 202
            
        };
        public enum STATUS_CODE:int
        {
            OK = 0,
            FAIL_UNKNOWN = 40000
        };
    }

    class Login : DataPackage
    {
        public class Data
        {
            public string user_name;
            public int user_port;
            public Data(string user_name,int user_port)
            {
                this.user_name = user_name;
                this.user_port = user_port;
            }
        }
        public new Data data;
        public Login(string user_name,int user_port)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.LOGIN;
            this.data = new Data(user_name,user_port);
        }
    }

    class LoginResponse : DataPackage
    {
        public class Data
        {
            public int status;
            public int user_id;
            public string user_name;
            public Data(int status,int user_id,string user_name)
            {
                this.status = status;
                this.user_id = user_id;
                this.user_name = user_name;
            }
        }
        public new Data data;
        public LoginResponse(int status, int user_id, string user_name)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.LOGIN_RESPONSE;
            this.data = new Data(status, user_id, user_name);
        }
    }

    class GetChatroomList : DataPackage
    {
        public class Data
        {
            public int user_id;
            public Data(int user_id)
            {
                this.user_id = user_id;
            }
        }
        public new Data data;
        public GetChatroomList(int user_id)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.GET_CHATROOM_LIST;
            this.data = new Data(user_id);
        }
    }

    class GetChatroomListResponse : DataPackage
    {
        public class Data
        {
            public int status;
            public ArrayList chatrooms;
            public Data(int status,ArrayList chatrooms)
            {
                this.status = status;
                this.chatrooms = chatrooms;
            }
        }
        public new Data data;
        public GetChatroomListResponse(int status, ArrayList chatrooms)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.GET_CHATROOM_LIST_RESPONSE;
            this.data = new Data(status, chatrooms);
        }
    }
    class JoinChatroom : DataPackage
    {
        public class Data
        {
            public int user_id;
            public int chatroom_id;
            public Data(int user_id, int chatroom_id)
            {
                this.user_id = user_id;
                this.chatroom_id = chatroom_id;
            }
        }
        public new Data data;
        public JoinChatroom(int user_id,int chatroom_id)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.JOIN_CHATROOM;
            this.data = new Data(user_id,chatroom_id);
        }

    }

    class JoinChatroomResponse : DataPackage
    {
        public class Data
        {
            public int status;
            public int chatroom_id;
            public ArrayList members;
            public Data(int status,int chatroom_id,ArrayList members)
            {
                this.status = status;
                this.chatroom_id = chatroom_id;
                this.members = members;
            }
        }
        public new Data data;
        public JoinChatroomResponse(int status, int chatroom_id, ArrayList members)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.JOIN_CHATROOM_RESPONSE;
            this.data = new Data(status, chatroom_id, members);
        }

    }

    class ExitChatroom : DataPackage
    {
        public class Data
        {
            public int user_id;
            public int chatroom_id;
            public Data(int user_id,int chatroom_id)
            {
                this.user_id = user_id;
                this.chatroom_id = chatroom_id;
            }
        }
        public new Data data;
        public ExitChatroom(int user_id,int chatroom_id)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.EXIT_CHATROOM;
            this.data = new Data(user_id,chatroom_id);
        }
    }
    class ExitChatroomResponse : DataPackage
    {
        public class Data
        {
            public int status;
            public int chatroom_id;
            public Data(int status,int chatroom_id)
            {
                this.status = status;
                this.chatroom_id = chatroom_id;
            }
        }
        public new Data data;
        public ExitChatroomResponse(int status,int chatroom_id)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.EXIT_CHATROOM_RESPONSE;
            this.data = new Data(status, chatroom_id);
        }

    }
    class ChatMessage : DataPackage
    {
        public class Data
        {
            public int user_id;
            public int type;
            public ArrayList receiver_id;
            public string message_content;
            public Data(int user_id,int type,ArrayList receiver_id,string message_content)
            {
                this.user_id = user_id;
                this.type = type;
                this.receiver_id = receiver_id;
                this.message_content = message_content;
            }
        }
        public new Data data;
        public ChatMessage(int user_id, int type, ArrayList receiver_id, string message_content)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.CHAT_MESSAGE;
            this.data = new Data(user_id, type, receiver_id, message_content);
        }
    }

    class PushMessage : DataPackage
    {
        public class Data
        {
            public int status;
            public int sender_id;
            public int chatroom_id;
            public string message_content;
            public Data(int status,int sender_id, int chatroom_id, string message_content)
            {
                this.status = status;
                this.sender_id = sender_id;
                this.chatroom_id = chatroom_id;
                this.message_content = message_content;
            }
        }
        public new Data data;
        public PushMessage(int status,int sender_id,int chatroom_id,string message_content)
        {
            this.message_code = (int)DataPackage.MESSAGE_CODE.PUSH_MESSAGE;
            this.data = new Data(status,sender_id,chatroom_id,message_content);

        }
    }
    class UpdateChatrooms : DataPackage
    {

    }





}
