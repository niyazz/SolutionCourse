using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public class Review
    {
        public string carNumber;

        public string Text;

        public Review()
        { }

        public Review(string carNumber, string text)
        {
            this.carNumber = carNumber;
            this.Text = text;
        }
    }
}
