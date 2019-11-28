
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public class Cars
    {
        public int Car_region{ get; set; }
        public string Car_describtion { get; set; }
        public int Car_old { get; set; }
        public string Car_numbers { get; set; }
        public string Car_brand { get; set; }
        public string Car_mark { get; set; }
        public int id_of_owner { get; set; }
        public User User { get; set; }
        public Cars()
        {

        }
        
        public Cars(int Car_region, string Car_numbers, string Car_brand, string Car_mark, int Car_old, string Car_describtion, int id_of_owner)
        {
            this.Car_brand = Car_brand;
            this.Car_describtion = Car_describtion;
            this.Car_mark = Car_mark;
            this.Car_region = Car_region;
            this.Car_numbers = Car_numbers;
            this.Car_old = Car_old;
            this.id_of_owner = id_of_owner;
        }
    }
}
