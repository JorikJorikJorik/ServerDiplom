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
    public class DialogController : ApiController
    {

        DialogLogic logic = new DialogLogic();

        [Route("api/Dialog")]
        [HttpGet]
        public DialogModel getMessageListByPart(int count, int number, string keyDialog)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.getMessageListByPartLogic(authorization.getUserId(), count, number, keyDialog);
        }

        [Route("api/Dialog/Send")]
        [HttpPost]
        public void sendMessage(MessageSendModel model, string keyDialog)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.sendMessageLogic(authorization.getUserId(), model, keyDialog);
        }

        [Route("api/Dialog/Delete")]
        [HttpDelete]
        public void deleteMessage(MessageDeleteListModel model)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.deleteMessageLogic(authorization.getUserId(), model);
        }

        [Route("api/Dialog/IsRead")]
        [HttpPut]
        public void IsRead(string keyDialog)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.isReadMessageLogic(authorization.getUserId(), keyDialog);
        }
    }
}