using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSwagger.Models
{
    public class ErrorResponse
    {
        public Error error { get; set; }
        public FieldErrors fieldErrors { get; set; }
    }
}
