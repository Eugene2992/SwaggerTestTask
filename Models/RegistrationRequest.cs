using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestSwagger.Models
{
    public class RegistrationRequest
    {
        public string registrationDate { get; set; }
        public string locale { get; set; }
        public Person person { get; set; }
        public Organisation organisation { get; set; }
        public string CreateRequest(RegistrationRequest registrationRequest, MyMemoryCache _requestCache)
        {
            string jsonString = JsonSerializer.Serialize<RegistrationRequest>(registrationRequest);
            Guid guid = Guid.NewGuid();
            string registrationId = guid.ToString();
            registrationRequest = _requestCache.GetOrCreate(registrationId,registrationRequest);
            return (registrationId);
        }

        public RegistrationRequest GetRequest(string registrationId, MyMemoryCache _requestCache)
        {
            var registrationRequest = new RegistrationRequest();
            registrationRequest = _requestCache.Get(registrationId, registrationRequest);
            return (registrationRequest);
        }
    }

  
}
