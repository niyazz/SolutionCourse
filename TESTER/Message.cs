using System;
using System.Collections.Generic;
using System.Text;

namespace TESTER
{
    public class Message
    {
        public int senderID { get; set; }
        public int adressID { get; set; }
        public string senderName { get; set; } // плохая практика, но так уменьшим количесвто запросов
        public string adressName { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public Message()
        {
        }
        public Message(string sName, DateTime dt, string text)
        {
            this.senderName = sName;
            this.Time = dt;
            this.Text = text;
        }
        public Message(int sId, int aId, string sName, string aName, DateTime dt, string text)
        {
            this.senderID = sId;
            this.adressID = aId;
            this.senderName = sName;
            this.adressName = aName;
            this.Time = dt;
            this.Text = text;
        }
    }
}
