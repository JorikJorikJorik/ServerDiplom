using System.Web.Http;
using KursWebApplication.Models;
using KursWebApplication.Business_Logic;
using System.Net;
using System.Net.Http;


namespace KursWebApplication.Controllers
{
    public class SignUpController : ApiController
    {
        SignUpLogic logic = new SignUpLogic();

        [Route("api/SignUp")]
        [HttpPost]
        public PreviewProfileModel SingUpUser(SignUpModel model) {
            GenerateResponce responce = new GenerateResponce(Request);

            //if (model != null)
            {
                PreviewProfileModel modelProfile = logic.signUpUser(model);
                if (modelProfile != null)
                    responce.generateResponce(HttpStatusCode.OK);
                else responce.generateThrowWithMessage(HttpStatusCode.Unauthorized, "User with this login exist");

                return modelProfile;
            }
            //return responce.generateError(HttpStatusCode.InternalServerError, "Empty data");
        }
    }
}