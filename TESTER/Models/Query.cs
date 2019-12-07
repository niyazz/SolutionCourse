using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTER.Models;

namespace TESTER
{
    public class Query
    {
        public string Type { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
        public Message Message { get; set; }
        public List<Message> Messages { get; set; }
        public Review Review { get; set; }
        public List<Review> Reviews { get; set; }
        public classNews New { get; set; }
        public List<classNews> News { get; set; }
        public List<Car> Cars { get; set; }
        
        public List<Friends> Friends { get; set; }
        
        
        public Query()
        { }
        public Query(string type)
        {
            this.Type = type;
        }
        public Query(string type, User obj)
        {
            this.Type = type;
            this.User = obj;
        }
        public Query(string type, Message obj)
        {
            this.Type = type;
            this.Message = obj;
        }
        public Query(string type, classNews obj)
        {
            this.Type = type;
            this.New = obj;
        }
        public Query(string type, Review obj)
        {
            this.Type = type;
            this.Review = obj;
        }
        public Query(string type, Car obj)
        {
            this.Type = type;
            this.Car = obj;
        }
        public Query(string type, User obj1, Car obj2)
        {
            this.Type = type;
            this.User = obj1;
            this.Car = obj2;
        }
        public Query(string type, List<Message> array)
        {
            this.Type = type;
            this.Messages = array;
        }

        public Query(string type, List<Review> array)
        {
            this.Type = type;
            this.Reviews = array;
        }
        public Query(string type, List<Car> array)
        {
            this.Type = type;
            this.Cars =  array;
        }
        public Query(string type, List<classNews> array)
        {
            this.Type = type;
            this.News = array;
        }
    }
}
