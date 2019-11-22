using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTER;

namespace TESTER
{
    class Query
    {
        public string Type { get; set; }
        public User User { get; set; }
        public Message Message { get; set; }
        public List<Message> Messages { get; set; }
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
