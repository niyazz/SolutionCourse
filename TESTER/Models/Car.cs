using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTER
{
    public class Car
    {
        public string Car_id { get; set; }
        public string Car_region { get; set; }
        public string Car_describtion { get; set; }
        public int Car_old { get; set; }
        public string Car_numbers { get; set; }
        public string Car_brand { get; set; }
        public string Car_mark { get; set; }
        public int id_of_owner { get; set; }
        public User User { get; set; }
        public Car()
        { }

        public Car(string id, string region, string description, int old, string numbers, string model, string brand, int user_id)
        {
            this.Car_id = id;
            this.Car_brand = brand;
            this.Car_describtion = description;
            this.Car_mark = model;
            this.Car_region = region;
            this.Car_numbers = numbers;
            this.Car_old = old;
            this.id_of_owner = user_id;
        }
    }
}
