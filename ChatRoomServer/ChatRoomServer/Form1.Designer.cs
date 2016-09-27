namespace ChatRoomServer
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortTextbox = new System.Windows.Forms.TextBox();
            this.ServerStopButton = new System.Windows.Forms.Button();
            this.ServerStartButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChatroomTextbox = new System.Windows.Forms.TextBox();
            this.AddRoomButton = new System.Windows.Forms.Button();
            this.ChatroomClearButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ChatroomListbox = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LogTextbox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ServerStartButton);
            this.groupBox1.Controls.Add(this.ServerStopButton);
            this.groupBox1.Controls.Add(this.PortTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口";
            // 
            // PortTextbox
            // 
            this.PortTextbox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PortTextbox.Location = new System.Drawing.Point(42, 18);
            this.PortTextbox.MaxLength = 5;
            this.PortTextbox.Name = "PortTextbox";
            this.PortTextbox.Size = new System.Drawing.Size(86, 21);
            this.PortTextbox.TabIndex = 1;
            // 
            // ServerStopButton
            // 
            this.ServerStopButton.Location = new System.Drawing.Point(68, 45);
            this.ServerStopButton.Name = "ServerStopButton";
            this.ServerStopButton.Size = new System.Drawing.Size(60, 23);
            this.ServerStopButton.TabIndex = 2;
            this.ServerStopButton.Text = "停止";
            this.ServerStopButton.UseVisualStyleBackColor = true;
            this.ServerStopButton.Click += new System.EventHandler(this.ServerStopButton_Click);
            // 
            // ServerStartButton
            // 
            this.ServerStartButton.Location = new System.Drawing.Point(6, 45);
            this.ServerStartButton.Name = "ServerStartButton";
            this.ServerStartButton.Size = new System.Drawing.Size(60, 23);
            this.ServerStartButton.TabIndex = 3;
            this.ServerStartButton.Text = "启动";
            this.ServerStartButton.UseVisualStyleBackColor = true;
            this.ServerStartButton.Click += new System.EventHandler(this.ServerStartButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChatroomClearButton);
            this.groupBox2.Controls.Add(this.AddRoomButton);
            this.groupBox2.Controls.Add(this.ChatroomTextbox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 79);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "聊天室";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "名称";
            // 
            // ChatroomTextbox
            // 
            this.ChatroomTextbox.Location = new System.Drawing.Point(42, 18);
            this.ChatroomTextbox.Name = "ChatroomTextbox";
            this.ChatroomTextbox.Size = new System.Drawing.Size(86, 21);
            this.ChatroomTextbox.TabIndex = 1;
            // 
            // AddRoomButton
            // 
            this.AddRoomButton.Location = new System.Drawing.Point(6, 45);
            this.AddRoomButton.Name = "AddRoomButton";
            this.AddRoomButton.Size = new System.Drawing.Size(60, 23);
            this.AddRoomButton.TabIndex = 2;
            this.AddRoomButton.Text = "创建";
            this.AddRoomButton.UseVisualStyleBackColor = true;
            this.AddRoomButton.Click += new System.EventHandler(this.AddRoomButton_Click);
            // 
            // ChatroomClearButton
            // 
            this.ChatroomClearButton.Location = new System.Drawing.Point(68, 45);
            this.ChatroomClearButton.Name = "ChatroomClearButton";
            this.ChatroomClearButton.Size = new System.Drawing.Size(60, 23);
            this.ChatroomClearButton.TabIndex = 3;
            this.ChatroomClearButton.Text = "重置";
            this.ChatroomClearButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ChatroomListbox);
            this.groupBox3.Location = new System.Drawing.Point(13, 185);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(134, 409);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "聊天室列表";
            // 
            // ChatroomListbox
            // 
            this.ChatroomListbox.FormattingEnabled = true;
            this.ChatroomListbox.ItemHeight = 12;
            this.ChatroomListbox.Location = new System.Drawing.Point(7, 21);
            this.ChatroomListbox.Name = "ChatroomListbox";
            this.ChatroomListbox.Size = new System.Drawing.Size(120, 376);
            this.ChatroomListbox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LogTextbox);
            this.groupBox4.Location = new System.Drawing.Point(154, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(550, 581);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "日志";
            // 
            // LogTextbox
            // 
            this.LogTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.LogTextbox.Location = new System.Drawing.Point(6, 20);
            this.LogTextbox.MaxLength = 1000000;
            this.LogTextbox.Multiline = true;
            this.LogTextbox.Name = "LogTextbox";
            this.LogTextbox.ReadOnly = true;
            this.LogTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogTextbox.Size = new System.Drawing.Size(538, 549);
            this.LogTextbox.TabIndex = 0;
            this.LogTextbox.TextChanged += new System.EventHandler(this.LogTextbox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 602);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ServerStartButton;
        private System.Windows.Forms.Button ServerStopButton;
        private System.Windows.Forms.TextBox PortTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ChatroomClearButton;
        private System.Windows.Forms.Button AddRoomButton;
        private System.Windows.Forms.TextBox ChatroomTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox ChatroomListbox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox LogTextbox;
    }
}

