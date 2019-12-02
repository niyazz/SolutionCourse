using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public class Review
    {
        public int UserID;

        public string Text;

        public Review()
        { }

        public Review(int userId, string text)
        {
            this.UserID = userId;
            this.Text = text;
        }
    }
}
