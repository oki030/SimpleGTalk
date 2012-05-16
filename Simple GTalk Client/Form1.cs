using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GTalkLib;

namespace Simple_GTalk_Client
{
    public partial class Form1 : Form
    {
        public List<ContactControl> ContactList = new List<ContactControl>();
        public List<ChatForm> ChatFormList = new List<ChatForm>();

        DebugForm dForm = new DebugForm();

        public Form1()
        {
            InitializeComponent();

            EmailTextBox.Focus();

            //dForm.Show();
        }

        public void CreateNewChatForm(String JID, String ContactName, String Message)
        {
            foreach (ChatForm c in ChatFormList)
            {
                if (c.JID == JID)
                    try
                    {
                        c.Focus();
                        return;
                    }
                    catch (Exception) { }
            }
     
            ChatForm cf = new ChatForm();
            cf.JID = JID;
            cf.ContactName = ContactName;
            cf.Text = "Chat with " + ContactName;
            if (Message != "") cf.CreateMessage = "<b>" + ContactName + "</b>: " + Message;
            cf.MainForm = this;

            ChatFormList.Add(cf);           
            cf.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: gTalkClient.PresenceStatus = UserPresenceStatus.Online; break;
                case 1: gTalkClient.PresenceStatus = UserPresenceStatus.Away; break;
                case 2: gTalkClient.PresenceStatus = UserPresenceStatus.Busy; break;         
            }

            gTalkClient.Connect(EmailTextBox.Text, PassTextBox.Text);
        }

        private void gTalkClient_Error(string ErrorMessage, int ErrorCode)
        {
            if (ErrorCode == 0) MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gTalkClient.Disconnect();
        }

        private void gTalkClient_LogIn()
        {
            LoginPanel.Visible = false;
            MainPanel.Visible = true;

            switch (gTalkClient.PresenceStatus)
            {
                case UserPresenceStatus.Online: StatusLabel.ForeColor = Color.Green; StatusLabel.Text = "Online"; break;
                case UserPresenceStatus.Away: StatusLabel.ForeColor = Color.OrangeRed; StatusLabel.Text = "Away"; break;
                case UserPresenceStatus.Busy: StatusLabel.ForeColor = Color.Red; StatusLabel.Text = "Busy"; break;
                case UserPresenceStatus.Offline: StatusLabel.ForeColor = Color.Gray; StatusLabel.Text = "Offline"; break;
            }

            foreach (ContactInfo ci in gTalkClient.ContactList)
            {               
                ContactControl c = new ContactControl();
                c.Dock = DockStyle.Top;
                c.Visible = true;

                ListPanel.Controls.Add(c);                

                ContactList.Add(c);

                c.Email = ci.Email;
                c.NameLabel.Text = ci.Name;
                c.MainForm = this;
            }
        }

        private void gTalkClient_PresenceChanged(string From, string StatusText, UserPresenceStatus Status)
        {                       
            foreach (ContactControl cc in ContactList)
            {
                if (From.IndexOf(cc.Email) != -1)
                {
                    cc.JID = From;

                    cc.StatusTextLabel.Text = StatusText;                    
                    cc.StatusLabel.Text = Status.ToString();

                    if (Status == UserPresenceStatus.Online)
                    {
                        cc.StatusLabel.ForeColor = Color.Green;
                        ListPanel.Controls.SetChildIndex(cc, ListPanel.Controls.Count - 1);
                    }

                    if (Status == UserPresenceStatus.Offline)
                    {
                        cc.StatusLabel.ForeColor = Color.Gray;
                        ListPanel.Controls.SetChildIndex(cc, 0);
                    }

                    if (Status == UserPresenceStatus.Away)
                    {
                        cc.StatusLabel.ForeColor = Color.OrangeRed;
                        ListPanel.Controls.SetChildIndex(cc, ListPanel.Controls.Count - 1);
                    }

                    if (Status == UserPresenceStatus.Busy)
                    {
                        cc.StatusLabel.ForeColor = Color.Red;
                        ListPanel.Controls.SetChildIndex(cc, ListPanel.Controls.Count - 1);
                    }
                }

                foreach (ContactControl oc in ContactList)
                {
                    if (oc.StatusLabel.Text == "Online") ListPanel.Controls.SetChildIndex(oc, ListPanel.Controls.Count - 1);
                }
            }

            if (From.IndexOf(EmailTextBox.Text) != -1)// User status update from another client
            {
                textBox3.Text = StatusText;                
                if (textBox3.Text == "") textBox3.Text = "<Enter status text here>";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void gTalkClient_NewMessage(string From, string Message, ChatStateStatus ChatState)
        {
            foreach (ChatForm cf in ChatFormList)
            {
                if (From == cf.JID)
                {
                    if (Message != "")
                    {
                        cf.AddNewMessage("<b>" + cf.ContactName + "</b>: " + Message, ChatState);
                    }
                    else
                    {
                        cf.AddNewMessage("", ChatState);
                    }
                    return;
                }                
            }

            String ContactName = "";
            foreach (ContactControl cc in ContactList)
            {
                if (From.IndexOf(cc.Email) != -1) ContactName = cc.NameLabel.Text;
            }

            if (ContactName == "") ContactName = From;
            if (Message != "") 
                CreateNewChatForm(From, ContactName, Message);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.KeyChar = '\0';
                gTalkClient.StatusText = textBox3.Text;

                ListPanel.Focus();

                e.Handled = true;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "<Enter status text here>") textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {            
            if (textBox3.Text != gTalkClient.StatusText) gTalkClient.StatusText = textBox3.Text;
            if (textBox3.Text == "") textBox3.Text = "<Enter status text here>";
            textBox3.ForeColor = Color.Gray;
        }
       
        private void gTalkClient_LogOut()
        {
            LoginPanel.Visible = true;
            MainPanel.Visible = false;

            foreach (ContactControl cc in ContactList)
            {
                cc.Dispose();
            }
            ContactList.Clear();

            foreach (ChatForm cf in ChatFormList)
            {
                cf.Dispose();
            }
            ChatFormList.Clear();
        }

        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gTalkClient.Disconnect();

            switch (gTalkClient.PresenceStatus)
            {
                case UserPresenceStatus.Online: StatusLabel.ForeColor = Color.Green; StatusLabel.Text = "Online"; break;
                case UserPresenceStatus.Away: StatusLabel.ForeColor = Color.OrangeRed; StatusLabel.Text = "Away"; break;
                case UserPresenceStatus.Busy: StatusLabel.ForeColor = Color.Red; StatusLabel.Text = "Busy"; break;
                case UserPresenceStatus.Offline: StatusLabel.ForeColor = Color.Gray; StatusLabel.Text = "Offline"; break;
            }
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gTalkClient.PresenceStatus = UserPresenceStatus.Online;

            switch (gTalkClient.PresenceStatus)
            {
                case UserPresenceStatus.Online: StatusLabel.ForeColor = Color.Green; StatusLabel.Text = "Online"; break;
                case UserPresenceStatus.Away: StatusLabel.ForeColor = Color.OrangeRed; StatusLabel.Text = "Away"; break;
                case UserPresenceStatus.Busy: StatusLabel.ForeColor = Color.Red; StatusLabel.Text = "Busy"; break;
                case UserPresenceStatus.Offline: StatusLabel.ForeColor = Color.Gray; StatusLabel.Text = "Offline"; break;
            }
        }

        private void awayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gTalkClient.PresenceStatus = UserPresenceStatus.Away;

            switch (gTalkClient.PresenceStatus)
            {
                case UserPresenceStatus.Online: StatusLabel.ForeColor = Color.Green; StatusLabel.Text = "Online"; break;
                case UserPresenceStatus.Away: StatusLabel.ForeColor = Color.OrangeRed; StatusLabel.Text = "Away"; break;
                case UserPresenceStatus.Busy: StatusLabel.ForeColor = Color.Red; StatusLabel.Text = "Busy"; break;
                case UserPresenceStatus.Offline: StatusLabel.ForeColor = Color.Gray; StatusLabel.Text = "Offline"; break;
            }
        }

        private void busyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gTalkClient.PresenceStatus = UserPresenceStatus.Busy;

            switch (gTalkClient.PresenceStatus)
            {
                case UserPresenceStatus.Online: StatusLabel.ForeColor = Color.Green; StatusLabel.Text = "Online"; break;
                case UserPresenceStatus.Away: StatusLabel.ForeColor = Color.OrangeRed; StatusLabel.Text = "Away"; break;
                case UserPresenceStatus.Busy: StatusLabel.ForeColor = Color.Red; StatusLabel.Text = "Busy"; break;
                case UserPresenceStatus.Offline: StatusLabel.ForeColor = Color.Gray; StatusLabel.Text = "Offline"; break;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainPopUp.Show(Location.X + pictureBox3.Location.X + 10, Location.Y + pictureBox3.Location.Y + 40);
        }

        private void gTalkClient_VCard(string From, string ContactName, byte[] ImageData, string ImageType)
        {
            Stream sr = new MemoryStream(ImageData);

            foreach (ContactControl cc in ContactList)
            {
                if (From.IndexOf(cc.Email) != -1)
                {
                    cc.pictureBox1.Image = Image.FromStream(sr);
                    cc.NameLabel.Text = ContactName;
                }
            }

            if (From.IndexOf(EmailTextBox.Text) != -1)
            {
                pictureBox2.Image = Image.FromStream(sr);
                DisplayNameLabel.Text = ContactName;
            }
        }

        private void gTalkClient_DebugMsg(string Message)
        {
            dForm.richTextBox1.Text += Message + "\n\n";
            dForm.richTextBox1.Update();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
