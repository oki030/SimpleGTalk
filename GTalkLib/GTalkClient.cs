using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace GTalkLib
{    
    public partial class GTalkClient : Component
    {
        Socket tcpClient;
        String Buff = "";
        String UserName = "";
        String Pass = "";
        String JID = "";

        bool Connected = false;
        bool Logged = false;
        bool SessionStarted = false;

        public String Author
        {
            get
            {
                return "Oliver Marković, 2012";
            }
        }

        List<ContactInfo> FContactList = new List<ContactInfo>();
        public List<ContactInfo> ContactList
        {
            get
            {
                return FContactList;
            }
        }

        String FStatusText = "";
        public String StatusText
        {
            set
            {
                FStatusText = value;

                if (SessionStarted)
                {
                    SetPresenceStatus(FStatusText, FPresenceStatus);
                }
            }

            get
            {
                return FStatusText;
            }
        }

        UserPresenceStatus FPresenceStatus = UserPresenceStatus.Online;
        public UserPresenceStatus PresenceStatus
        {
            set
            {
                FPresenceStatus = value;

                if (SessionStarted)
                {
                    SetPresenceStatus(FStatusText, FPresenceStatus);
                }
            }

            get
            {
                return FPresenceStatus;
            }
        }

        #region Events

        public event DebugMsgHandler DebugMsg;
        public event NewMessageHandler NewMessage;
        public event PresenceChangedHandler PresenceChanged;
        public event EventHandler LogIn;
        public event EventHandler LogOut;
        public event ErrorMsgHandler Error;
        public event VCardHandler VCard;

        protected virtual void OnDebugMsg(String Msg)
        {
            try
            {
                if (DebugMsg != null)
                {
                    Control control = DebugMsg.Target as Control;
                    if (control != null && control.InvokeRequired)
                    {
                        control.Invoke(DebugMsg, Msg);
                    }
                    else
                    {
                        DebugMsg(Msg);
                    }
                }
            }
            catch (Exception) { }
        }

        protected virtual void OnNewMessage(String From, String Message, ChatStateStatus ChatState)
        {
            try
            {
                if (NewMessage != null)
                {
                    Control control = NewMessage.Target as Control;
                    if (control != null && control.InvokeRequired)
                    {
                        control.Invoke(NewMessage, new object[] { From, Message, ChatState });
                    }
                    else
                    {
                        NewMessage(From, Message, ChatState);
                    }
                }
            }
            catch (Exception) { }
        }

        protected virtual void OnPresenceChanged(String From, String StatusText, UserPresenceStatus Status)
        {
            try
            {
                if (PresenceChanged != null)
                {
                    Control control = PresenceChanged.Target as Control;
                    if (control != null && control.InvokeRequired)
                    {
                        control.Invoke(PresenceChanged, new object[] { From, StatusText, Status });
                    }
                    else
                    {
                        PresenceChanged(From, StatusText, Status);
                    }
                }
            }
            catch (Exception) { }
        }

        protected virtual void OnLogIn()
        {
            try
            {
                if (LogIn != null)
                {
                    Control control = LogIn.Target as Control;
                    if (control != null && control.InvokeRequired)
                    {
                        control.Invoke(LogIn);
                    }
                    else
                    {
                        LogIn();
                    }
                }
            }
            catch (Exception) { }
        }

        protected virtual void OnLogOut()
        {
            try
            {
                if (LogOut != null)
                {
                    Control control = LogOut.Target as Control;
                    if (control != null && control.InvokeRequired)
                    {
                        control.Invoke(LogOut);
                    }
                    else
                    {
                        LogOut();
                    }
                }
            }
            catch (Exception) { }
        }

        protected virtual void OnError(String ErrorMsg, int ErrorCode)
        {
            if (Error != null)
            {
                try
                {
                    Control control = Error.Target as Control;
                    if (control != null && control.InvokeRequired)
                    {
                        control.Invoke(Error, new object[] {ErrorMsg, ErrorCode});
                    }
                    else
                    {
                        Error(ErrorMsg, ErrorCode);
                    }
                }
                catch (Exception) { }
            }
        }

        protected virtual void OnVCard(String From, String ContactName, byte[] ImageData, String ImageType)
        {
            if (VCard != null)
            {
                Control control = VCard.Target as Control;
                if (control != null && control.InvokeRequired)
                {
                    control.Invoke(VCard, new object[] { From, ContactName, ImageData, ImageType });
                }
                else
                {
                    VCard(From, ContactName, ImageData, ImageType);
                }
            }
        }
        
        #endregion

        #region Public Methods

        public GTalkClient()
        {
            InitializeComponent();
        }

        public GTalkClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Connect(String UserName, String Password)
        {
            if (!Connected)
            {
                this.UserName = UserName;
                this.Pass = Password;

                new Thread(thrFun).Start();
            }
        }

        public void Disconnect()
        {
            if (Connected)
            {
                try
                {
                    tcpClient.Close();
                }
                catch (Exception) { }
            }
        }

        public void SendChatState(String To, ChatStateStatus ChatState)
        {
            switch (ChatState)
            {
                case ChatStateStatus.Active: SendXml("<message from=\"" + JID + "\" to=\"" + To + "\" type=\"chat\"><active xmlns=\"http://jabber.org/protocol/chatstates\"/></message>"); break;
                case ChatStateStatus.Composing: SendXml("<message from=\"" + JID + "\" to=\"" + To + "\" type=\"chat\"><composing xmlns=\"http://jabber.org/protocol/chatstates\"/></message>"); break;
                case ChatStateStatus.Gone: SendXml("<message from=\"" + JID + "\" to=\"" + To + "\" type=\"chat\"><gone xmlns=\"http://jabber.org/protocol/chatstates\"/></message>"); break;
                case ChatStateStatus.Inactive: SendXml("<message from=\"" + JID + "\" to=\"" + To + "\" type=\"chat\"><inactive xmlns=\"http://jabber.org/protocol/chatstates\"/></message>"); break;
                case ChatStateStatus.Paused: SendXml("<message from=\"" + JID + "\" to=\"" + To + "\" type=\"chat\"><paused xmlns=\"http://jabber.org/protocol/chatstates\"/></message>"); break;
            }
        }

        public void SendMessage(String To, String Message)
        {
            SendXml("<message from=\"" + JID + "\" to=\"" + To + "\" type=\"chat\"><active xmlns=\"http://jabber.org/protocol/chatstates\"/><body>" + Message.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;") + "</body></message>");
        }

        public void SendXml(String Xml)
        {
            try
            {
                byte[] Data = System.Text.Encoding.UTF8.GetBytes(Xml);
                if (Connected) tcpClient.Send(Data);

                OnDebugMsg("Sent: " + Xml);
            }
            catch (Exception) { }
        }

        #endregion

        void thrFun()
        {
            try
            {
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpClient.Connect("talk.google.com", 5222);

                if (tcpClient.Connected)
                {
                    Connected = true;
                    SendXml("<?xml version=\"1.0\"?><stream:stream  to=\"google.com\" version=\"1.0\" xmlns=\"jabber:client\" xmlns:stream=\"http://etherx.jabber.org/streams\">");
                }

                while (tcpClient.Connected)
                {

                    String Xml = "";
                    byte[] Data = new byte[4096];

                    if (tcpClient.Receive(Data) != 0)
                    {
                        Xml = System.Text.Encoding.UTF8.GetString(Data).Trim(new char[] { '\0' });

                        if (Xml != "")
                            try
                            {
                                OnDebugMsg(Xml);

                                if (!Logged)
                                {
                                    DoLogin(Xml);
                                }
                                else
                                {
                                    DoXml(Xml);
                                }
                            }
                            catch (Exception) { }
                    }
                }
            }
            catch (Exception e)
            {
                OnDebugMsg("Error: " + e.Message);
            }

            Disconnect();
            Buff = "";
            Connected = false;
            Logged = false;
            SessionStarted = false;
            FContactList.Clear();

            OnLogOut(); // Fire LogOut Event            
        }

        #region Process XML

        #region TagFun

        bool CanIGetTag(String StartStr, String EndStr)
        {
            bool ret = false;
            String s = "";

            int startIndex = Buff.IndexOf(StartStr);

            if (startIndex != -1)
            {
                try
                {
                    s = Buff.Substring(startIndex);

                    if (s.IndexOf(EndStr) != -1)
                    {
                        if (EndStr == "/>")
                        {
                            if (s.Substring(1, s.IndexOf(EndStr) - 1).IndexOf("<") == -1) ret = true;
                        }
                        else
                        {
                            ret = true;
                        }
                    }
                }
                catch (Exception) { }
            }

            return ret;
        }

        bool CanIGetTag(String Xml, String StartStr, String EndStr)
        {
            bool ret = false;
            String s = "";

            int startIndex = Xml.IndexOf(StartStr);

            if (startIndex != -1)
            {
                try
                {
                    s = Xml.Substring(startIndex);

                    if (s.IndexOf(EndStr) != -1)
                    {
                        if (EndStr == "/>")
                        {
                            if (s.Substring(1, s.IndexOf(EndStr) - 1).IndexOf("<") == -1) ret = true;
                        }
                        else
                        {
                            ret = true;
                        }
                    }
                }
                catch (Exception) { }
            }

            return ret;
        }

        String GetTag(String StartStr, String EndStr)
        {
            String ret = "";

            try
            {
                ret = Buff.Substring(Buff.IndexOf(StartStr));

                Buff = Buff.Substring(0, Buff.IndexOf(StartStr)) + ret.Substring(ret.IndexOf(EndStr) + EndStr.Length);

                ret = ret.Substring(0, ret.IndexOf(EndStr) + EndStr.Length);
            }
            catch (Exception) { }

            return ret;
        }

        String GetTag(ref String Xml, String StartStr, String EndStr)
        {
            String ret = "";

            try
            {
                ret = Xml.Substring(Xml.IndexOf(StartStr));

                Xml = Xml.Substring(0, Xml.IndexOf(StartStr)) + ret.Substring(ret.IndexOf(EndStr) + EndStr.Length);

                ret = ret.Substring(0, ret.IndexOf(EndStr) + EndStr.Length);
            }
            catch (Exception) { }

            return ret;
        }

        #endregion

        void DoLogin(String Xml)
        {
            Buff += Xml;            

            if (Buff.IndexOf("X-GOOGLE-TOKEN") != -1)
            {               
                String auth = "";
                String s = "";

                WebClient wc = new WebClient();

                try
                {
                    s = wc.DownloadString("https://www.google.com/accounts/ClientLogin?accountType=GOOGLE&Email=" + UserName + "&Passwd=" + Pass + "&service=mail");
                }
                catch (Exception) { }

                if (s == "")
                {
                    OnError("Login failed.", 0);

                    try
                    {                        
                        tcpClient.Close();
                    }
                    catch (Exception) { }
                    return;
                }

                if (s.IndexOf("Auth=") > 0) auth = s.Substring(s.IndexOf("Auth=") + 5);

                auth = Convert.ToBase64String(Encoding.ASCII.GetBytes("\0" + UserName + "\0" + auth));

                SendXml("<auth xmlns=\"urn:ietf:params:xml:ns:xmpp-sasl\" mechanism=\"X-GOOGLE-TOKEN\">" + auth + "</auth>");
                Buff = "";
            }

            if (Buff.IndexOf("<success xmlns=\"urn:ietf:params:xml:ns:xmpp-sasl\"/>") != -1)
            {
                Buff = "";
                SendXml("<?xml version=\"1.0\"?><stream:stream  to=\"google.com\" version=\"1.0\" xmlns=\"jabber:client\" xmlns:stream=\"http://etherx.jabber.org/streams\">");
            }

            if (Buff.IndexOf("<failure xmlns=\"urn:ietf:params:xml:ns:xmpp-sasl\">") != -1)
            {
                OnError("Login failed.", 0);

                try
                {
                    tcpClient.Close();
                }
                catch (Exception) { }
                return;
            }

            if (Buff.IndexOf("<bind") != -1)
            {
                Buff = "";
                Logged = true;
                SendXml("<iq id=\"bid_resource\" type=\"set\"><bind xmlns=\"urn:ietf:params:xml:ns:xmpp-bind\"><resource>home</resource></bind></iq>");
            }
        }

        void DoXml(String Xml)
        {
            Buff += Xml;

            // IQ
            while (CanIGetTag("<iq", "</iq>")) DoIQTag(GetTag("<iq", "</iq>"));
            while (CanIGetTag("<iq", "/>")) DoIQTag(GetTag("<iq", "/>"));

            // Presence
            while (CanIGetTag("<presence", "</presence>")) DoPresenceTag(GetTag("<presence", "</presence>"));
            while (CanIGetTag("<presence", "/>")) DoPresenceTag(GetTag("<presence", "/>"));

            // Message
            while (CanIGetTag("<message", "</message>")) DoMessageTag(GetTag("<message", "</message>"));
            while (CanIGetTag("<message", "/>")) DoMessageTag(GetTag("<message", "/>"));
        }

        void DoIQTag(String Xml)
        {                   
            if (Xml.IndexOf("id=\"bid_resource\"") != -1)
            {
                if (Xml.IndexOf("<jid>") != -1)
                {
                    JID = Xml.Substring(Xml.IndexOf("<jid>") + 5);
                    JID = JID.Substring(0, JID.IndexOf("</jid>"));
                }

                Buff = "";
                // Start Session
                SendXml("<iq id=\"start_session\" type=\"set\"><session xmlns=\"urn:ietf:params:xml:ns:xmpp-session\"/></iq>");
            }

            if (Xml.IndexOf("id=\"start_session\"") != -1)
            {
                Buff = "";
                SessionStarted = true;

                // Get contact list (Roster)
                SendXml("<iq id=\"roster_req\" type=\"get\"><query xmlns=\"jabber:iq:roster\"/></iq>");
            }

            if (Xml.IndexOf("id=\"roster_req\"") != -1)
            {
                String cpXml = Xml.Trim(new char[] { '\0', '\n' });

                while (CanIGetTag(cpXml, "<item", "/>"))
                {
                    String sUserName = GetTag(ref cpXml, "<item", "/>");
                    String sName = sUserName;

                    try
                    {                        
                        // Only true friends :)
                        if (sUserName.IndexOf("subscription=\"both\"") != -1)
                        {
                            sUserName = sUserName.Substring(sUserName.IndexOf("jid=\"") + 5);
                            sUserName = sUserName.Substring(0, sUserName.IndexOf("\""));

                            if (sName.IndexOf("name=\"") != -1)
                            {
                                sName = sName.Substring(sName.IndexOf("name=\"") + 6);
                                sName = sName.Substring(0, sName.IndexOf("\""));
                            }
                            else sName = sUserName;

                            FContactList.Add(new ContactInfo(sName, sUserName.ToLower()));
                            SendXml("<iq from=\"" + JID + "\" to=\"" + sUserName + "\" id=\"vcard\" type=\"get\"><vCard xmlns=\"vcard-temp\"/></iq>");
                        }
                    }
                    catch (Exception) { }
                }

                SendXml("<iq from=\"" + JID + "\" to=\"" + JID + "\" id=\"vcard\" type=\"get\"><vCard xmlns=\"vcard-temp\"/></iq>");

                OnLogIn(); // Fire LoggedIn event
                SetPresenceStatus(FStatusText, FPresenceStatus);
            }

            if (Xml.IndexOf("id=\"vcard\"") != -1)
            {
                if (Xml.IndexOf("type=\"error\"") != -1) return;

                try
                {
                    String From = Xml.Substring(Xml.IndexOf("from=\"") + 6);
                    From = From.Substring(0, From.IndexOf("\""));

                    String Binval = GetTag(ref Xml, "<BINVAL>", "</BINVAL>");
                    Binval = Binval.Substring(8, Binval.Length - 17);

                    String Type = GetTag(ref Xml, "<TYPE>", "</TYPE>");
                    Type = Type.Substring(6, Type.Length - 13);

                    String ContactName = GetTag(ref Xml, "<FN>", "</FN>");
                    ContactName = ContactName.Substring(4, ContactName.Length - 9);

                    foreach (ContactInfo ci in FContactList)
                    {
                        if (From.IndexOf(ci.Email) != -1)
                        {
                            ci.Name = ContactName;
                        }
                    }

                    OnVCard(From, ContactName, Convert.FromBase64String(Binval), Type);
                }
                catch (Exception) { }                
            }
        }

        void DoPresenceTag(String Xml)
        {
            if (Xml.IndexOf("type=\"error\"") != -1) return;

            String From = "";
            String StatusText = "";
            String Status = "Online";

            From = Xml.Substring(Xml.IndexOf("from=\"") + 6);
            From = From.Substring(0, From.IndexOf("\""));

            if (Xml.IndexOf("<status/>") == -1)
            {
                if (Xml.IndexOf("<status>") != -1)
                {
                    StatusText = Xml.Substring(Xml.IndexOf("<status>") + 8);
                    StatusText = StatusText.Substring(0, StatusText.IndexOf("</status>"));
                }
            }

            if (Xml.IndexOf("type=\"unavailable\"") != -1) Status = "Offline";
            if (Xml.IndexOf("<show>away</show>") != -1) Status = "Away";
            if (Xml.IndexOf("<show>dnd</show>") != -1) Status = "Busy";

            UserPresenceStatus upStatus = UserPresenceStatus.Online;

            switch (Status)
            {
                case "Online": upStatus = UserPresenceStatus.Online; break;
                case "Offline": upStatus = UserPresenceStatus.Offline; break;
                case "Away": upStatus = UserPresenceStatus.Away; break;
                case "Busy": upStatus = UserPresenceStatus.Busy; break;
            }

            OnPresenceChanged(From.ToLower(), StatusText.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'"), upStatus);
        }

        void DoMessageTag(String Xml)
        {
            if (Xml.IndexOf("type=\"error\"") != -1) return;

            String From = "";
            String Msg = "";
            ChatStateStatus chatStatus = ChatStateStatus.Active;

            if (Xml.IndexOf("<composing") != -1) chatStatus = ChatStateStatus.Composing;
            if (Xml.IndexOf("<active") != -1) chatStatus = ChatStateStatus.Active;
            if (Xml.IndexOf("<gone") != -1) chatStatus = ChatStateStatus.Gone;
            if (Xml.IndexOf("<inactive") != -1) chatStatus = ChatStateStatus.Inactive;
            if (Xml.IndexOf("<paused") != -1) chatStatus = ChatStateStatus.Paused;

            if (Xml.IndexOf("from=\"") != -1)
            {
                From = Xml.Substring(Xml.IndexOf("from=\"") + 6);
                From = From.Substring(0, From.IndexOf("\""));
            }

            if (Xml.IndexOf("<body>") != -1)
            {
                Msg = Xml.Substring(Xml.IndexOf("<body>") + 6);
                Msg = Msg.Substring(0, Msg.IndexOf("</body>"));
            }

            OnNewMessage(From.ToLower(), Msg.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'"), chatStatus);
        }

        #endregion

        #region Private Methods

        void SetPresenceStatus(String StatusText, UserPresenceStatus Status)
        {
            String st = "<status/>";
            if (StatusText != "") st = "<status>" + StatusText.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;") + "</status>";

            switch (Status)
            {
                case UserPresenceStatus.Online: SendXml("<presence><show></show>" + st + "</presence>"); break;
                case UserPresenceStatus.Offline: SendXml("<presence type=\"unavailable\"/>"); break;
                case UserPresenceStatus.Away: SendXml("<presence><show>away</show>" + st + "</presence>"); break;
                case UserPresenceStatus.Busy: SendXml("<presence><show>dnd</show>" + st + "</presence>"); break;
            }
        }

        #endregion
    }
}