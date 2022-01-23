using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TestSwagger.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {

        private RegistrationRequest _registrationRequest;
        private RegistrationRequest _registrationResponse;
        private GetRegistrationResponse _registrationResponseToGetMethod;
        private ErrorResponse _errorResponse;
        private Error _errorResponseError;
        private FieldErrors _fieldErrors;
        private MyMemoryCache _requestCache = new MyMemoryCache();

        // GET api/<Registrations>/
        [HttpGet]
        public ActionResult Get(string registrationId)
        {
            
            try
            {
                _registrationResponse = new RegistrationRequest();
                _registrationResponseToGetMethod = new GetRegistrationResponse();
                _registrationResponse = _registrationResponse.GetRequest(registrationId, _requestCache);
                //если получаем объект с полями null, значит не найшло запись по id 
                if ((_registrationResponse.locale != null) & (_registrationResponse.organisation != null) & (_registrationResponse.person != null))
                {
                    //нашло запись по id, инициализирую экземпляр класса GetRegistrationResponse
                    _registrationResponseToGetMethod.id = registrationId;
                    _registrationResponseToGetMethod.locale = _registrationResponse.locale;
                    _registrationResponseToGetMethod.organisation = _registrationResponse.organisation;
                    _registrationResponseToGetMethod.person = _registrationResponse.person;
                    _registrationResponseToGetMethod.registrationDate = _registrationResponse.registrationDate;
                    return StatusCode(200, JsonConvert.SerializeObject(_registrationResponseToGetMethod));
                }
                else
                {
                    _errorResponse = new ErrorResponse();
                    _errorResponseError = new Error();
                    _errorResponseError.code = "404";
                    _errorResponseError.errorMessage = "Registration record haven't been found due provided id, please enter the existing one"; 
                    _errorResponse.error = _errorResponseError;  
                    return StatusCode(404, JsonConvert.SerializeObject(_errorResponse));
                }
               
            } 
            catch
            {
                _errorResponse = new ErrorResponse();
                _errorResponseError = new Error();
                _errorResponseError.code = "500";
                _errorResponseError.errorMessage = "Registration record haven't been found because of the failure of the server, please try again later";
                _errorResponse.error = _errorResponseError;
                return StatusCode(500, JsonConvert.SerializeObject(_registrationResponseToGetMethod));

            }
           
        }

        // POST api/<Registrations>
        [HttpPost]
        public ActionResult Post(string value)
        {
            string registrationId = "";
            _registrationRequest = new RegistrationRequest();
            try
            {
                //сознание заявки, с возвращением уникального id 
                registrationId = _registrationRequest.CreateRequest(JsonConvert.DeserializeObject<RegistrationRequest>(value.ToString()), _requestCache);
                RegistrationResponse postReply = new RegistrationResponse();
                postReply.registrationId = registrationId;
                return StatusCode(201, JsonConvert.SerializeObject(postReply));
            }
            catch (Exception ex)
            {
                _errorResponse = new ErrorResponse();
                _errorResponseError = new Error();
                _fieldErrors = new FieldErrors();
                _errorResponseError.code = "ValidationFailed";
                _errorResponseError.errorMessage = null;
                _fieldErrors.code = "400";
                _fieldErrors.field = ex.Message;
                _fieldErrors.message = "Smth bad in your record";
                _errorResponse.error = _errorResponseError;
                _errorResponse.fieldErrors = _fieldErrors;
                return StatusCode(400, JsonConvert.SerializeObject(_errorResponse));
            }
        }




    }
}
