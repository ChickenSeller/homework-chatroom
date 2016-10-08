using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ChatroomClient
{
    class ClientHandler
    {
        static public void HandleGetChatroomListResponse(GetChatroomListResponse package, EndPoint point)
        {
            if (package.data.status == 0)
            {
                Program.client.chatrooms.chatrooms = package.data.chatrooms;
                Program.client.formMain.RefreshChatroomList();
            }
        }

        static public void HandleJoinChatroomResponse(JoinChatroomResponse package, EndPoint point)
        {
            if (package.data.status != 0)
            {
                return;
            }
            int index = -1;
            int num = Program.client.chatrooms.chatrooms.Count;
            for (int i = 0; i < num; i++)
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
            Program.client.RefreshChatroomForms();
        }

        static public void HandlePushMessage(PushMessage package,EndPoint point)
        {
            if (package.data.status != 0)
            {
                return;
            }
            int index = -1;
            int num = Program.client.chatrooms.chatrooms.Count;
            for (int i = 0; i < num; i++)
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
            int roomID = Program.client.GetFormChatroom((ChatroomNode)Program.client.chatrooms.chatrooms[index]);
            if (roomID == -1)
            {
                return;
            }
            if (((FormChatroomNode)(Program.client.formChatrooms[roomID])).is_running == false)
            {
                return;
            }
            ((FormChatroomNode)(Program.client.formChatrooms[roomID])).form.RefreshChatMessage(package.data.sender_id, package.data.message_content);
        }
    }
}
