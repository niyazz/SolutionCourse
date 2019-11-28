using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTER
{
    public class Cars
    {
        public string Car_name { get; set; }
        public string Car_describtion { get; set; }
        public int Car_old { get; set; }
        public string Car_numbers { get; set; }
        public string Car_brand { get; set; }
        public string Car_mark { get; set; }
        public string login_of_owner { get; set; }
        public User User { get; set; }
        public Cars()
        {

        }
        public Cars(string Car_name, string Car_numbers, string Car_brand, string Car_mark, int Car_old, string Car_describtion, string login_of_owner)
        {
            this.Car_brand = Car_brand;
            this.Car_describtion = Car_describtion;
            this.Car_mark = Car_mark;
            this.Car_name = Car_name;
            this.Car_numbers = Car_numbers;
            this.Car_old = Car_old;
            this.login_of_owner = login_of_owner;
        }
    }
}
