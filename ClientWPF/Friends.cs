using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public class Friends
    {
        public int UserID { get; set; }

        public int FriendID { get; set; }

        public Friends()
        {

        }
        public Friends(int userId, int friendId )
        {
            this.FriendID = friendId;
            this.UserID = userId;
        }
    }
}
