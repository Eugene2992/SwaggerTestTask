using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSwagger.Models
{
    public class GetRegistrationResponse
    {
        public string id { get; set; }
        public string registrationDate { get; set; }
        public string locale { get; set; }
        public Person person { get; set; }
        public Organisation organisation { get; set; }



    }


}
