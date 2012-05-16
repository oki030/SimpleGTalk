namespace Simple_GTalk_Client
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EmailTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PassTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.DisplayNameLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.MainPopUp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.awayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.busyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gTalkClient = new GTalkLib.GTalkClient(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.LoginPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.InfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.MainPopUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.Location = new System.Drawing.Point(30, 262);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(196, 20);
            this.EmailTextBox.TabIndex = 0;
            this.EmailTextBox.Text = "@gmail.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User name:";
            // 
            // PassTextBox
            // 
            this.PassTextBox.Location = new System.Drawing.Point(31, 316);
            this.PassTextBox.Name = "PassTextBox";
            this.PassTextBox.Size = new System.Drawing.Size(196, 20);
            this.PassTextBox.TabIndex = 2;
            this.PassTextBox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 399);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Online",
            "Away",
            "Busy"});
            this.comboBox1.Location = new System.Drawing.Point(67, 354);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(208, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.pictureBox1);
            this.LoginPanel.Controls.Add(this.EmailTextBox);
            this.LoginPanel.Controls.Add(this.comboBox1);
            this.LoginPanel.Controls.Add(this.label1);
            this.LoginPanel.Controls.Add(this.button2);
            this.LoginPanel.Controls.Add(this.PassTextBox);
            this.LoginPanel.Controls.Add(this.button1);
            this.LoginPanel.Controls.Add(this.label2);
            this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginPanel.Location = new System.Drawing.Point(0, 0);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(254, 472);
            this.LoginPanel.TabIndex = 8;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.ListPanel);
            this.MainPanel.Controls.Add(this.InfoPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(254, 472);
            this.MainPanel.TabIndex = 9;
            this.MainPanel.Visible = false;
            // 
            // ListPanel
            // 
            this.ListPanel.AutoScroll = true;
            this.ListPanel.BackColor = System.Drawing.Color.White;
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(0, 44);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(254, 428);
            this.ListPanel.TabIndex = 1;
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.InfoPanel.Controls.Add(this.DisplayNameLabel);
            this.InfoPanel.Controls.Add(this.StatusLabel);
            this.InfoPanel.Controls.Add(this.pictureBox3);
            this.InfoPanel.Controls.Add(this.pictureBox2);
            this.InfoPanel.Controls.Add(this.textBox3);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.InfoPanel.Location = new System.Drawing.Point(0, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(254, 44);
            this.InfoPanel.TabIndex = 0;
            // 
            // DisplayNameLabel
            // 
            this.DisplayNameLabel.AutoSize = true;
            this.DisplayNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayNameLabel.ForeColor = System.Drawing.Color.Black;
            this.DisplayNameLabel.Location = new System.Drawing.Point(58, 2);
            this.DisplayNameLabel.Name = "DisplayNameLabel";
            this.DisplayNameLabel.Size = new System.Drawing.Size(0, 15);
            this.DisplayNameLabel.TabIndex = 4;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.ForeColor = System.Drawing.Color.Green;
            this.StatusLabel.Location = new System.Drawing.Point(20, 4);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(37, 13);
            this.StatusLabel.TabIndex = 3;
            this.StatusLabel.Text = "Online";
            this.StatusLabel.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(10, 14);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(215, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // textBox3
            // 
            this.textBox3.AcceptsReturn = true;
            this.textBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Gray;
            this.textBox3.Location = new System.Drawing.Point(23, 23);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(186, 20);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "<Enter status text here>";
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // MainPopUp
            // 
            this.MainPopUp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.awayToolStripMenuItem,
            this.busyToolStripMenuItem,
            this.toolStripMenuItem1,
            this.offlineToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.MainPopUp.Name = "contextMenuStrip1";
            this.MainPopUp.Size = new System.Drawing.Size(153, 148);
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.onlineToolStripMenuItem.Text = "Online";
            this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
            // 
            // awayToolStripMenuItem
            // 
            this.awayToolStripMenuItem.Name = "awayToolStripMenuItem";
            this.awayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.awayToolStripMenuItem.Text = "Away";
            this.awayToolStripMenuItem.Click += new System.EventHandler(this.awayToolStripMenuItem_Click);
            // 
            // busyToolStripMenuItem
            // 
            this.busyToolStripMenuItem.Name = "busyToolStripMenuItem";
            this.busyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.busyToolStripMenuItem.Text = "Busy";
            this.busyToolStripMenuItem.Click += new System.EventHandler(this.busyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // offlineToolStripMenuItem
            // 
            this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
            this.offlineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.offlineToolStripMenuItem.Text = "Offline";
            this.offlineToolStripMenuItem.Click += new System.EventHandler(this.offlineToolStripMenuItem_Click);
            // 
            // gTalkClient
            // 
            this.gTalkClient.PresenceStatus = GTalkLib.UserPresenceStatus.Online;
            this.gTalkClient.StatusText = "";
            this.gTalkClient.DebugMsg += new GTalkLib.DebugMsgHandler(this.gTalkClient_DebugMsg);
            this.gTalkClient.NewMessage += new GTalkLib.NewMessageHandler(this.gTalkClient_NewMessage);
            this.gTalkClient.PresenceChanged += new GTalkLib.PresenceChangedHandler(this.gTalkClient_PresenceChanged);
            this.gTalkClient.LogIn += new GTalkLib.EventHandler(this.gTalkClient_LogIn);
            this.gTalkClient.LogOut += new GTalkLib.EventHandler(this.gTalkClient_LogOut);
            this.gTalkClient.Error += new GTalkLib.ErrorMsgHandler(this.gTalkClient_Error);
            this.gTalkClient.VCard += new GTalkLib.VCardHandler(this.gTalkClient_VCard);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(254, 472);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LoginPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(270, 9999);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple GTalk Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.MainPopUp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox EmailTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PassTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Panel ListPanel;
        public GTalkLib.GTalkClient gTalkClient;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ContextMenuStrip MainPopUp;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem awayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem busyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label DisplayNameLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

