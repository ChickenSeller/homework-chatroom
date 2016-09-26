namespace ChatroomClient
{
    partial class FormChatroom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChatHistory_TextBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Draft_TextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChatHistory_TextBox
            // 
            this.ChatHistory_TextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ChatHistory_TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ChatHistory_TextBox.Location = new System.Drawing.Point(4, 3);
            this.ChatHistory_TextBox.MaxLength = 214748647;
            this.ChatHistory_TextBox.Multiline = true;
            this.ChatHistory_TextBox.Name = "ChatHistory_TextBox";
            this.ChatHistory_TextBox.ReadOnly = true;
            this.ChatHistory_TextBox.Size = new System.Drawing.Size(429, 326);
            this.ChatHistory_TextBox.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(440, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(133, 460);
            this.listBox1.TabIndex = 1;
            // 
            // Draft_TextBox
            // 
            this.Draft_TextBox.Location = new System.Drawing.Point(4, 336);
            this.Draft_TextBox.Multiline = true;
            this.Draft_TextBox.Name = "Draft_TextBox";
            this.Draft_TextBox.Size = new System.Drawing.Size(429, 103);
            this.Draft_TextBox.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(358, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(277, 445);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(4, 445);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "退出聊天室";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // FormChatroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 472);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Draft_TextBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ChatHistory_TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormChatroom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormChatroom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatHistory_TextBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox Draft_TextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}