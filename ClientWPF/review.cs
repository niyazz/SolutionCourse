using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public class review
    {
        public int UserID;

        public string Review;

        public review()
        {

        }

        public review(int userId, string review)
        {
            this.UserID = userId;
            this.Review = review;
        }
    }
}
