using System;
using System.Collections.Generic;
using System.Text;
using TESTER;

namespace TESTER.Models
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
