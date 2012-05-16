using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTalkLib
{
    public class ContactInfo
    {
        public String Name;
        public String Email;

        public ContactInfo(String Name, String Email)
        {
            this.Name = Name;
            this.Email = Email;
        }
    }

    public enum UserPresenceStatus { Online, Offline, Away, Busy };
    public enum ChatStateStatus { Active, Inactive, Composing, Paused, Gone };

    public delegate void EventHandler();

    public delegate void ErrorMsgHandler(String ErrorMessage, int ErrorCode);

    public delegate void DebugMsgHandler(String Message);

    public delegate void NewMessageHandler(String From, String Message, ChatStateStatus ChatState);

    public delegate void PresenceChangedHandler(String From, String StatusText, UserPresenceStatus Status);    

    public delegate void VCardHandler(String From, String ContactName, byte[] ImageData, String ImageType);
}