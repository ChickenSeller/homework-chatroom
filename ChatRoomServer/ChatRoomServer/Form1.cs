using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRoomServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ServerStopButton.Enabled = false;
            this.ChatroomClearButton.Enabled = false;
            this.AddRoomButton.Enabled = true;

        }

        private void ServerStartButton_Click(object sender, EventArgs e)
        {
            if (this.PortTextbox.Text == "")
            {
                MessageBox.Show("端口不能为空");
                return;
            }
            int port=0;
            try
            {
                port = Convert.ToInt32(this.PortTextbox.Text);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
            if (port<=0 || port > 65535)
            {
                MessageBox.Show("端口必须在1-16635之间");
                return;
            }
            Program.server = new Server(port);
            int flag = Program.server.StartServer();
            if (flag == -1)
            {
                MessageBox.Show("操作失败");
                return;
            }
            else if (flag == 1)
            {
                MessageBox.Show("端口被占用，请修改");
                return;
            }else
            {
                //MessageBox.Show("操作成功");
                this.ServerStartButton.Enabled = false;
                this.ServerStopButton.Enabled = true;
                return;
            }
        }

        private void ServerStopButton_Click(object sender, EventArgs e)
        {
            if (!Program.server.RunningFlag)
            {
                MessageBox.Show("服务不在运行");
                return;
            }
            int flag = Program.server.StopServer();
            if (flag == -1)
            {
                MessageBox.Show("操作失败");
                return;
            }
            else
            {
                //MessageBox.Show("操作成功");
                this.ServerStartButton.Enabled = true;
                this.ServerStopButton.Enabled = false;
                return;
            }
        }

        public void Log(string msg)
        {
            string message = "[" + DateTime.Now.ToString() + "]\t" + msg + System.Environment.NewLine;
            this.LogTextbox.Text += message;
        }

        private void LogTextbox_TextChanged(object sender, EventArgs e)
        {
            LogTextbox.SelectionStart = LogTextbox.Text.Length;
            LogTextbox.ScrollToCaret();
        }

        private void AddRoomButton_Click(object sender, EventArgs e)
        {
            if (this.ChatroomTextbox.Text == "")
            {
                MessageBox.Show("聊天室名称不能为空");
                return;
            }
            Program.server.chatrooms.AddChatroom(this.ChatroomTextbox.Text);
        }
    }
}
