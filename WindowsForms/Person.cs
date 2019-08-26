using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Book
    {
        public string Name = "Element of Physics";
        public string Author = "R.S. Agrwal";
        public static int Price = 500;
        private string metadata = "djdieeiie";
    }
}