using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

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
            this.Text = Program.client.myself.UserName+"\t聊天室:" + chatroom.ChatroomName + "    成员:" + chatroom.ChatroomMembers.Count.ToString();
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
            this.Text = Program.client.myself.UserName + "    聊天室:" + this.chatroomNode.ChatroomName + "    成员:" + this.chatroomNode.ChatroomMembers.Count.ToString();
            this.MenberListbox.Items.Clear();
            foreach (UserNode tempNode in this.chatroomNode.ChatroomMembers)
            {
                this.MenberListbox.Items.Add(tempNode.UserName);
            }
        }

        public void RefreshChatMessage(int sender_id,string message_content)
        {
            foreach(UserNode temp in this.chatroomNode.ChatroomMembers)
            {
                if (sender_id == temp.UserID)
                {
                    this.ChatHistory_TextBox.Text = this.ChatHistory_TextBox.Text + ChatMessageFormat(temp.UserName,message_content);
                }
            }
        }

        private void FormChatroom_Load(object sender, EventArgs e)
        {
            JoinChatroom package = new JoinChatroom(Program.client.myself.UserID, this.chatroomNode.ChatroomID);
            Program.client.SendMsg(package, Program.client.serverip);
        }

        private string ChatMessageFormat(string sender_name,string message_content)
        {
            DateTime nowtime = DateTime.Now;
            string res ="["+ nowtime.Year.ToString() + "-" + nowtime.Month.ToString() + "-" + nowtime.Day.ToString() + " " + nowtime.Hour.ToString() + ":" + nowtime.Minute.ToString() + ":" + nowtime.Second.ToString()+"]";
            res = sender_name + "\t" + res;
            res = res + "\r\n" + message_content+"\r\n\r\n";
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Draft_TextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("发送消息不能为空！");
                return;
            }
            string msg_content = this.Draft_TextBox.Text.Trim();
            ArrayList rooms = new ArrayList();
            rooms.Add(this.chatroomNode.ChatroomID);
            ChatMessage msg = new ChatMessage(Program.client.myself.UserID, 0, rooms, msg_content);
            Program.client.SendMsg(msg, Program.client.serverip);
            this.Draft_TextBox.Clear();
        }

        private void ChatHistory_TextBox_TextChanged(object sender, EventArgs e)
        {
            this.ChatHistory_TextBox.SelectionStart = this.ChatHistory_TextBox.Text.Length;
            this.ChatHistory_TextBox.ScrollToCaret();
        }
    }

}
