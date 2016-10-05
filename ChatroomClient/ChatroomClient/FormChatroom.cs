using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatroomClient
{
    public partial class FormChatroom : Form
    {
        public FormChatroom()
        {
            InitializeComponent();
        }
        public FormChatroom(ChatroomNode chatroom)
        {
            InitializeComponent();
            this.chatroomNode = chatroom;
            this.Text = "聊天室:" + chatroom.ChatroomName + "    成员:" + chatroom.ChatroomMembers.Count.ToString();
        }

        private void FormChatroom_FormClosed(object sender, FormClosedEventArgs e)
        {
            int roomID = Program.client.GetFormChatroom(this.chatroomNode);
            Program.client.formChatrooms.RemoveAt(roomID);
            Application.ExitThread();
        }
        private ChatroomNode chatroomNode;

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void RefreshChatroomInfo()
        {
            foreach(ChatroomNode tempNode in Program.client.chatrooms.chatrooms)
            {
                if(tempNode.ChatroomID == this.chatroomNode.ChatroomID)
                {
                    this.chatroomNode = tempNode;
                    break;
                }
            }
            this.Text = "聊天室:" + this.chatroomNode.ChatroomName + "    成员:" + this.chatroomNode.ChatroomMembers.Count.ToString();
            this.MenberListbox.Items.Clear();
            foreach (UserNode tempNode in this.chatroomNode.ChatroomMembers)
            {
                this.MenberListbox.Items.Add(tempNode.UserName);
            }
        }

        private void FormChatroom_Load(object sender, EventArgs e)
        {
            JoinChatroom package = new JoinChatroom(Program.client.myself.UserID, this.chatroomNode.ChatroomID);
            Program.client.SendMsg(package, Program.client.serverip);
        }
    }

}
