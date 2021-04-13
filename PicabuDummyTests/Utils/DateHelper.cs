using System;
using System.Collections.Generic;
using System.Text;

namespace PicabuDummyTests.Utils
{
    class DateHelper
    {
        public string name { get; set; }
        public DateTime date { get; set; }

        public DateHelper(string name, DateTime date)
        {
            this.name = name;
            this.date = date;
        }
    }
}
