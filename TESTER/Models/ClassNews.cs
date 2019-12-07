using System;
using System.Collections.Generic;
using System.Text;

namespace TESTER.Models
{
    public class classNews
    {
        public int senderID { get; set; }
        public string senderName { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }

        public classNews()
        {
        }

        public classNews(string sName, DateTime dt, string text)
        {
            this.senderName = sName;
            this.Time = dt;
            this.Text = text;
        }
        public classNews(int senderID, string sName, DateTime dt, string text)
        {
            this.senderID = senderID;
            this.senderName = sName;
            this.Time = dt;
            this.Text = text;
        }
    }
}
