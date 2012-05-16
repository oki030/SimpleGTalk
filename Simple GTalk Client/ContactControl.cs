using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple_GTalk_Client
{
    public partial class ContactControl : UserControl
    {
        public String JID = "";
        public String Email = "";
        public Form1 MainForm;

        public ContactControl()
        {
            InitializeComponent();
        }

        private void ContactControl_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(255, 230, 230, 230);
            NameLabel.Enabled = false;
            StatusLabel.Enabled = false;
            StatusTextLabel.Enabled = false;
        }

        private void ContactControl_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
            NameLabel.Enabled = true;
            StatusLabel.Enabled = true;
            StatusTextLabel.Enabled = true;
        }

        private void ContactControl_DoubleClick(object sender, EventArgs e)
        {
            if (JID == "")
                MainForm.CreateNewChatForm(Email, NameLabel.Text, "");
            else
                MainForm.CreateNewChatForm(JID, NameLabel.Text, "");
        }
    }
}
