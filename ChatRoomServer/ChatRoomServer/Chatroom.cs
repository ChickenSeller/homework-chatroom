using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ChatRoomServer
{
    class Chatroom
    {
        public ArrayList chatrooms;
        public Chatroom()
        {
            this.chatrooms = new ArrayList();
        }
        public ChatroomNode AddChatroom(string ChatroomName)
        {
            ChatroomNode newChatroom = new ChatroomNode(ChatroomName);
            this.chatrooms.Add(newChatroom);
            return newChatroom;
        }
        public ChatroomNode AddChatroom(string ChatroomName,ArrayList ChatroomMember)
        {
            ChatroomNode newChatroom = new ChatroomNode(ChatroomName,ChatroomMember);
            this.chatrooms.Add(newChatroom);
            return newChatroom;
        }

        public ChatroomNode GetChatRoomByID(int id)
        {
            foreach(ChatroomNode tempChatroom in this.chatrooms)
            {
                if (tempChatroom.ChatroomID == id)
                {
                    return tempChatroom;
                }
            }
            return null;
        }
    }

    class ChatroomNode
    {
        public string ChatroomName;
        static public int ChatroomNum;
        public int ChatroomID;
        public ArrayList ChatroomMembers;
        public ChatroomNode(string ChatroomName,ArrayList ChatroomMember)
        {
            ChatroomNode.ChatroomNum += 1;
            this.ChatroomID = ChatroomNode.ChatroomNum;
            this.ChatroomName = ChatroomName;
            this.ChatroomMembers = ChatroomMember;
        }
        public ChatroomNode(string ChatroomName)
        {
            ChatroomNode.ChatroomNum += 1;
            this.ChatroomID = ChatroomNode.ChatroomNum;
            this.ChatroomName = ChatroomName;
            this.ChatroomMembers = new ArrayList();
        }
        public void AddMember(UserNode user)
        {
            if (this.ChatroomMembers.Contains(user))
            {
                return;
            }
            this.ChatroomMembers.Add(user);
            return;
        }
        public void DelMember(UserNode user)
        {
            if (this.ChatroomMembers.Contains(user))
            {
                this.ChatroomMembers.Remove(user);
            }
        }
    }
}
