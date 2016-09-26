using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace ChatroomClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text= Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public FormMain(UserNode myself,IPEndPoint serverip)
        {
            InitializeComponent();
            this.myself = myself;
            this.ServerInfoLabel.Text = serverip.Address.ToString() + ":" + serverip.Port.ToString();
            this.UserIDLabel.Text = myself.UserID.ToString();
            this.UserNameLabel.Text = myself.UserName.ToString();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }
        public UserNode myself;

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Environment.Exit(0);
        }

        private void 进入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChatroom chatroom = new FormChatroom();
            chatroom.Show();
        }
    }
}
