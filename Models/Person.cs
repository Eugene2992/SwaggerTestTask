using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSwagger.Models
{
    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
    }
}
