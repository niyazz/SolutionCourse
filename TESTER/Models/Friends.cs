using System;
using System.Collections.Generic;
using System.Text;

namespace TESTER.Models
{
    public class Friends
    {
        public int UserID { get; set; }
        public int FriendID { get; set; }
        public string friendName { get; set; }


        public Friends()
        { }
        public Friends(int userId, int friendId)
        {
            this.FriendID = friendId;
            this.UserID = userId;
        }
        public Friends(int friendId, string friendName)
        {
            this.FriendID = friendId;
            this.friendName = friendName;
        }
    }
}
