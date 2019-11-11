using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTER
{
    class User
    {
        public string User_login { get; set; }
        public string User_password { get; set; }
        public string User_name { get; set; }
        public string User_sername { get; set; }
        public string User_mail { get; set; }
        public string User_birthday { get; set; }
        public int User_litrs { get; set; }

        public string Type { get; set; }

        public User()
        {

        }
        public User(string type)
        {
            this.Type = type;
        }
        public User(string type, string login, string pass)
        {
            this.Type = type;
            this.User_login = login;
            this.User_password = pass;
        }
        public User(string type,string login, string name, string sername, string birthday, int litrs, string mail, string pass)
        {
            this.Type = type;
            this.User_login = login;
            this.User_name = name;
            this.User_sername = sername;
            this.User_birthday = birthday;
            this.User_litrs = litrs;
            this.User_mail = mail;
            this.User_password = pass;

        }
    }
}
