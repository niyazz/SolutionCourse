using System;
using System.Collections.Generic;
using System.Text;

namespace TESTER.Models
{
    public class Friends
    {
        public int UserID { get; set; }
        public int FriendID { get; set; }


        public Friends()
        { }
        public Friends(int userId, int friendId)
        {
            this.FriendID = friendId;
            this.UserID = userId;
        }
    }
}
