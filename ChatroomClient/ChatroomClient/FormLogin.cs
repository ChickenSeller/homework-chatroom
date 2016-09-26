using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ChatroomClient
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = Thread.CurrentThread.ManagedThreadId.ToString();
        }

        private void Connect_Button_Click(object sender, EventArgs e)
        {
            Program.client.InitServerInfo(this.ServerIP_TextBox.Text, Int32.Parse(this.ServerPort_TextBox.Text));
            Program.client.DoLogin(this.UserNameTextBox.Text);
            //FormMain formMain = new FormMain();
            //formMain.Show();
            //this.Close();
        }

        private void Exit_Button_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
