using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using KursWebApplication.Business_Logic;
using KursWebApplication.Business_Logic.Kurs3._2;
using KursWebApplication.Models;


namespace KursWebApplication.Controllers
{
    public class FirebaseController : ApiController
    {
        FirebaseLogic logic = new FirebaseLogic();

        [Route("api/Firebase/AddToken")]
        [HttpPost]
        public void addToken(string token)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.addTokenLogic(authorization.getUserId(), token);
        }

        [Route("api/Firebase/RefreshToken")]
        [HttpPost]
        public void refreshToken(string newToken)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.refreshTokenLogic(authorization.getUserId(), newToken);
        }
    }
}