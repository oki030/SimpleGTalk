using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GTalkLib;

namespace Simple_GTalk_Client
{    
    public partial class ChatForm : Form
    {
        [DllImport("user32.dll")]
        static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        int ChatStatusCount = 0;
        bool ChatStatusWrite = false;

        public String JID = "";
        public String ContactName = "";
        public String CreateMessage = "";

        public ChatForm()
        {
            InitializeComponent();            
        }

        public Form1 MainForm { get; set; }    

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.gTalkClient.SendChatState(JID, ChatStateStatus.Gone);
            MainForm.ChatFormList.Remove(this);
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            ChatStream.Navigate("about:blank");
        }        

        public void AddNewMessage(String Msg, ChatStateStatus ChatState)
        {                      
            switch (ChatState)
            {
                case ChatStateStatus.Active: Text = "Chat with " + ContactName; break;
                case ChatStateStatus.Composing: Text = ContactName + " is composing message..."; break;
                case ChatStateStatus.Gone: Text = ContactName + "has left the conversation."; break; 
                case ChatStateStatus.Paused: Text = ContactName + " has entered text"; break;
            }

            if (Msg == "") return;

            String sm = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\img\\";
            String HtmlMsg = Msg.Replace("\n", "</br>").Replace(":)", "<img src=\"" + sm + "1.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-)", "<img src=\"" + sm + "1.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":(", "<img src=\"" + sm + "2.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-(", "<img src=\"" + sm + "2.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":D", "<img src=\"" + sm + "3.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-D", "<img src=\"" + sm + "3.gif\" height=\"19\" width=\"19\"/>")
                .Replace(";)", "<img src=\"" + sm + "4.gif\" height=\"19\" width=\"19\"/>")
                .Replace(";-)", "<img src=\"" + sm + "4.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":P", "<img src=\"" + sm + "5.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-P", "<img src=\"" + sm + "5.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":'(", "<img src=\"" + sm + "6.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-|", "<img src=\"" + sm + "7.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-/", "<img src=\"" + sm + "8.gif\" height=\"19\" width=\"19\"/>")
                .Replace("<3", "<img src=\"" + sm + "9.gif\" height=\"19\" width=\"19\"/>")
                .Replace(":-o", "<img src=\"" + sm + "10.gif\" height=\"19\" width=\"19\"/>")
                .Replace("x-(", "<img src=\"" + sm + "11.gif\" height=\"19\" width=\"19\"/>")
                .Replace("B-)", "<img src=\"" + sm + "12.gif\" height=\"19\" width=\"19\"/>")
                ;

            ChatStream.Document.Body.InnerHtml += HtmlMsg + "</br>";

            ChatStream.Document.Window.ScrollTo(0, short.MaxValue);

            if (!textBox1.Focused)
            {
                FlashWindow(this.Handle, true);
                System.Media.SystemSounds.Exclamation.Play();
            }
        }      

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Control.ModifierKeys != Keys.Shift)
                {
                    if (textBox1.Text != "")
                    {
                        MainForm.gTalkClient.SendMessage(JID, textBox1.Text);                        
                        AddNewMessage("<font color=\"gray\"><b>Me</b></font>: " + textBox1.Text, ChatStateStatus.Active);
                        textBox1.Text = "";

                        ChatStatusWrite = false;
                        ChatTimer.Enabled = false;
                        MainForm.gTalkClient.SendChatState(JID, ChatStateStatus.Active);
                    }

                    e.KeyChar = '\0';
                }

                e.Handled = true;
            }
        }

        private void ChatStream_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (CreateMessage != "")
            {
                AddNewMessage(CreateMessage, ChatStateStatus.Active);
                CreateMessage = "";

                FlashWindow(this.Handle, true);
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!ChatStatusWrite)
            {
                ChatStatusWrite = true;
                ChatTimer.Enabled = true;
                MainForm.gTalkClient.SendChatState(JID, ChatStateStatus.Composing);                
            }

            ChatStatusCount = 0;                       
        }

        private void ChatTimer_Tick(object sender, EventArgs e)
        {
            ChatStatusCount ++;

            if (ChatStatusCount == 5)
            {
                ChatStatusCount = 0;
                MainForm.gTalkClient.SendChatState(JID, ChatStateStatus.Paused);
                ChatStatusWrite = false;
                ChatTimer.Enabled = false;
            }
        }        
    }
}
