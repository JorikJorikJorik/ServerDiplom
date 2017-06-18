using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using KursWebApplication.Models;
using KursWebApplication.Business_Logic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KursWebApplication.Controllers
{
    public class LogInController : ApiController
    {
        LogInLogic logic = new LogInLogic();

        [Route("api/LogIn")]
        [HttpGet]
        public PreviewProfileModel LogInUser()
        {
            GenerateResponce responce = new GenerateResponce(Request);
            PreviewProfileModel model = new PreviewProfileModel();
            if (Request.Headers.Contains("Authorization"))
            {
                string basic = Request.Headers.GetValues("Authorization").First();
                model = logic.logInUser(basic);

                if (model == null)
                {
                    responce.generateThrowWithMessage(HttpStatusCode.Unauthorized, "No SignUp user");
                }
                return model;
            }

            responce.generateThrowWithMessage(HttpStatusCode.BadRequest, "No Authorization field");
            return model;
        }
    }
}