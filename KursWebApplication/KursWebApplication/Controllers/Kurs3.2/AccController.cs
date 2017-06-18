using System;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

using KursWebApplication.Business_Logic;
using System.Web.Http;
using System.Collections.Generic;

namespace KursWebApplication.Controllers
{
    public class AccController : ApiController
    {
        AccLogic logic = new AccLogic();

        // POST api/values
      /*  [System.Web.Http.Route("api/AccountUser/Driver")]
        [System.Web.Http.HttpPost]
        public int PostDriver(Models.DriverAccountModel newDriver)
        {

            if (newDriver != null)
            {
                int number = logic.logicMethodForPostDataDriver(newDriver);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                if ((int)response.StatusCode == 200) return number;
                return (int)response.StatusCode;
            }

            return (int)Request.CreateResponse(HttpStatusCode.BadRequest).StatusCode;
        }

        [System.Web.Http.Route("api/AccountUser/Dispatcher")]
        [System.Web.Http.HttpPost]
        public int PostDispatcher(Models.DispatcherAccountModel newDispathcer)
        {

            if (newDispathcer != null)
            {
                int number = logic.logicMethodForPostDataDispatcher(newDispathcer);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
                if ((int)response.StatusCode == 200) return number;
                return (int)response.StatusCode;
            }

            return (int)Request.CreateResponse(HttpStatusCode.BadRequest).StatusCode;

        }

        [System.Web.Http.Route("api/SingIn")]
        [System.Web.Http.HttpPost]
        public List<string> PostSignIn(Models.AccountModel data)
        {
            return logic.logicMethodForPostSingDataDispatcher(data);
        }*/
    }
}