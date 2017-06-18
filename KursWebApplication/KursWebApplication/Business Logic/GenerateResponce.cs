using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using KursWebApplication.Models;
using KursWebApplication.Business_Logic;
using System.Net;
using System.Net.Http;
namespace KursWebApplication.Business_Logic
{
    public class GenerateResponce
    {
        private HttpRequestMessage request;

        public GenerateResponce(HttpRequestMessage request)
        {
            this.request = request;
        }

        public HttpResponseMessage generateError(HttpStatusCode code, string error)
        {
            return request.CreateResponse(code, new HttpError(error));
        }

        public HttpResponseMessage generateResponce(HttpStatusCode code)
        {
            return request.CreateResponse(code);
        }

        public void generateThrowWithMessage(HttpStatusCode code, string error) {
            var message = request.CreateErrorResponse(code, new HttpError(error));
            throw new HttpResponseException(message);
        }

        public void generateThrow(HttpStatusCode code)
        {
            throw new HttpResponseException(code);
        }
    }
}