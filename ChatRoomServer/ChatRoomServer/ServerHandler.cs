using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;

namespace ChatRoomServer
{
    class ServerHandler
    {
        public static void HandleLogin(Login package, EndPoint point)
        {
            UserNode user = Program.server.users.AddUser(((IPEndPoint)point).Address.ToString(), package.data.user_port, package.data.user_name);
            Program.server.users.UpdateUserIP(user.UserID, (IPEndPoint)point);
            LoginResponse response = new LoginResponse((int)DataPackage.STATUS_CODE.OK, user.UserID, user.UserName);
            Program.server.Log("用户登录:\t" + user.UserIP + ":" + user.UserPort.ToString() + "\tID:" + user.UserID.ToString() + "\tName:" + user.UserName);
            Program.server.SendMsg(response, point);
            UpdateUsers res = new UpdateUsers(0, Program.server.users.users);
            Program.server.BroadcastMsg(res, Program.server.users.users);
        }
        public static void HandleGetChatroomList(GetChatroomList package, EndPoint point)
        {
            GetChatroomListResponse response = new GetChatroomListResponse((int)DataPackage.STATUS_CODE.OK, Program.server.chatrooms.chatrooms);
            UserNode user = Program.server.users.GetUserByID(package.data.user_id);
            Program.server.users.UpdateUserIP(user.UserID, (IPEndPoint)point);
            Program.server.Log("获取聊天室列表:\t" + user.UserIP + ":" + user.UserPort.ToString() + "\tID:" + user.UserID.ToString() + "\tName:" + user.UserName);
            Program.server.SendMsg(response, point);

        }

        public static void HandleJoinChatroom(JoinChatroom package, EndPoint point)
        {
            ChatroomNode chatroom = Program.server.chatrooms.GetChatRoomByID(package.data.chatroom_id);
            int index = Program.server.chatrooms.chatrooms.IndexOf(chatroom);
            UserNode user = Program.server.users.GetUserByID(package.data.user_id);
            chatroom.AddMember(user);
            Program.server.users.UpdateUserIP(user.UserID, (IPEndPoint)point);
            Program.server.chatrooms.chatrooms.RemoveAt(index);
            Program.server.chatrooms.chatrooms.Insert(index, chatroom);
            JoinChatroomResponse response = new JoinChatroomResponse((int)DataPackage.STATUS_CODE.OK, chatroom.ChatroomID, chatroom.ChatroomMembers);
            Program.server.Log("加入聊天室:\t:" + user.UserIP + ":" + user.UserPort.ToString() + "\tChatroom ID:" + chatroom.ChatroomID.ToString());
            Program.server.BroadcastMsg(response, chatroom.ChatroomMembers);
        }
        public static void HandleExitChatroom(ExitChatroom package, EndPoint point)
        {
            ChatroomNode chatroom = Program.server.chatrooms.GetChatRoomByID(package.data.chatroom_id);
            int index = Program.server.chatrooms.chatrooms.IndexOf(chatroom);
            UserNode user = Program.server.users.GetUserByID(package.data.user_id);
            chatroom.DelMember(user);
            Program.server.users.UpdateUserIP(user.UserID, (IPEndPoint)point);
            Program.server.chatrooms.chatrooms.RemoveAt(index);
            Program.server.chatrooms.chatrooms.Insert(index, chatroom);
            ExitChatroomResponse response = new ExitChatroomResponse((int)DataPackage.STATUS_CODE.OK, chatroom.ChatroomID);
            Program.server.SendMsg(response, point);
        }


        public static void HandleChatMessage(ChatMessage package, EndPoint point)
        {
            ArrayList targetChatrooms = package.data.receiver_id;
            Program.server.users.UpdateUserIP(package.data.user_id, (IPEndPoint)point);
            PushMessage msg = new PushMessage(0, package.data.user_id, 0, package.data.message_content);
            foreach(long id in targetChatrooms)
            {
                ChatroomNode tempNode = Program.server.chatrooms.GetChatRoomByID(Convert.ToInt32(id));
                msg.data.chatroom_id = tempNode.ChatroomID;
                Program.server.Log("发送消息:\tUser ID:" + package.data.user_id + "\tChatroom ID:" + package.data.receiver_id[0]);
                Program.server.BroadcastMsg(msg, tempNode.ChatroomMembers);
            }
        }

        
    }
}
