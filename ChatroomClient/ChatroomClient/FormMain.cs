﻿using System;
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
using System.Collections;

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
        }
        public UserNode myself;

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 进入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChatroom chatroom = new FormChatroom();
            chatroom.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            GetChatroomList getChatroomList = new GetChatroomList(myself.UserID);
            Program.client.SendMsg(getChatroomList, Program.client.serverip);
        }

        public void RefreshChatroomList()
        {
            int count = Program.client.chatrooms.chatrooms.Count;
            ChatroomNode tempNode;
            this.Chatroom_ListBox.Items.Clear();
            for(int i = 0; i < count; i++)
            {
                tempNode = (ChatroomNode)Program.client.chatrooms.chatrooms[i];
                this.Chatroom_ListBox.Items.Insert(i, tempNode.ChatroomName);
            }
        }

        private void Chatroom_ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.Chatroom_ListBox.SelectedIndex;
            ChatroomNode roomNode = (ChatroomNode)Program.client.chatrooms.chatrooms[index];
            Program.client.AddFormChatroom(roomNode);
            int roomID = Program.client.GetFormChatroom(roomNode);
            ParameterizedThreadStart ParStart = new ParameterizedThreadStart(Program.client.openFormChatroom);
            Thread myThread = new Thread(ParStart);
            myThread.Start(roomNode);
        }
    }
}
