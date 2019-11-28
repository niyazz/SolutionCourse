using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientWPF
{
    public class Query
    {
        public string Type { get; set; }
        public User User { get; set; }
        public Cars car { get; set; }
        public Message Message { get; set; }
        public List<Message> Messages { get; set; }

        public List<Friends> friends { get; set; }

        public List<review> reviews { get; set; }
        public Query()
        { }
        public Query(string type)
        {
            this.Type = type;
        }
        public Query(string type, User obj, Cars obj2)
        {
            this.Type = type;
            this.User = obj;
            this.car = obj2;
        }
        public Query(string type,  Cars obj2)
        {
            this.Type = type;
            this.car = obj2;
        }
        public Query(string type, User obj)
        {
            this.Type = type;
            this.User = obj;
        }
        public Query(string type, List<Message> array)
        {
            this.Type = type;
            this.Messages = array;
        }
        public Query(string type, Message obj)
        {
            this.Type = type;
            this.Message = obj;
        }
    }
}
