using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace KursWebApplication.Business_Logic.Kurs3._2
{
    public class AuthorizationUtils//<T>
    {
        private string keyAuthorization = "Authorization";
        private HttpRequestMessage request;
        private GenerateResponce responce;
        //private T model;

        public AuthorizationUtils(/*T model,*/ HttpRequestMessage request, GenerateResponce responce) {
            this.request = request;
            this.responce = responce;
            //this.model = model;
        }

        public AuthorizationUtils(/*T model,*/ HttpRequestMessage request)
        {
            this.request = request;
            this.responce = new GenerateResponce(request);
            //this.model = model;
        }

        /*  public T responceData(Func<int, T> logicFunc)
          {
              if (!request.Headers.Contains(keyAuthorization))
              {
                  responce.generateThrowWithMessage(HttpStatusCode.BadRequest, "No Authorization field");
              }

              int id = getAuthorizationUserId();
              model = logicFunc(id);
              return model;
          }*/

        public int getUserId()
        {
            if (!request.Headers.Contains(keyAuthorization))
            {
                responce.generateThrowWithMessage(HttpStatusCode.BadRequest, "No Authorization field");
            }
            int userId = getAuthorizationUserId();
            updateLastActiveTime(userId);
            return userId;
        }

        private int getAuthorizationUserId()
        {
            int userId = 0;
            var db = new MyDBModels.DB();
            string basic = request.Headers.GetValues(keyAuthorization).First();

            try {
                var  resultSearch = db.signUp.Where(s => s.BaseCode == basic);
                if (resultSearch.Count() == 0)
                {
                    responce.generateThrowWithMessage(HttpStatusCode.Unauthorized, "No SignUp user");
                }
                userId = resultSearch.First().UserId;
            }
            catch (Exception exc) {
                responce.generateThrowWithMessage(HttpStatusCode.Unauthorized, "No SignUp user");
            }

            return userId;

        }

        private void updateLastActiveTime(int userId)
        {
            var db = new MyDBModels.DB();
            var userModel = db.user.Where(u => u.UserId == userId).First();
            var profileModel = db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();
            profileModel.TimeLastActive = DateTime.Now;
            db.SaveChanges();
        }
    }
}