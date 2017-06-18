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
    public class CommunicationController : ApiController
    {

        CommunicationLogic logic = new CommunicationLogic();

        [Route("api/Communication")]
        [HttpGet]
        public List<CommunicationDataModel> getCommunicationListByPart(int count, int number)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.getCommunicationListByPartLogic(authorization.getUserId(), count, number);
        }

        [Route("api/Communication/CreateChat")]
        [HttpPost]
        public void createCommunication(CommunicationShortModel model)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.createCommunicationLogic(authorization.getUserId(), model);
        }

        [Route("api/Communication/DeleteChat")]
        [HttpPost]
        public void deleteCommunication(string keyDialog)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.deleteCommunicationLogic(authorization.getUserId(), keyDialog);
        }

        [Route("api/Communication/LeaveChat")]
        [HttpPost]
        public void leaveCommunication(string keyDialog)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.leaveCommunicationLogic(authorization.getUserId(), keyDialog);
        }

        [Route("api/Communication/ClearHistory")]
        [HttpPost]
        public void clearHistory(string keyDialog)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.clearHistoryLogic(authorization.getUserId(), keyDialog);
        }

        [Route("api/Communication/PinToTop")]
        [HttpPost]
        public void pinToTop(string keyDialog, bool isPin)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.pinToTopLogic(authorization.getUserId(), keyDialog, isPin);
        }

        [Route("api/Communication/ChangeInfo")]
        [HttpPut]
        public void settingChangeInfoCommunication(string keyDialog, CommunicationChangeModel model)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.settingChangeInfoCommunicationLogic(authorization.getUserId(), keyDialog, model);
        }

        [Route("api/Communication/AddUser")]
        [HttpPost]
        public void settingAddProfileToCommunication(string keyDialog, int profileId)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.settingAddProfileToCommunicationLogic(authorization.getUserId(), keyDialog, profileId);
        }

        [Route("api/Communication/DeleteUser")]
        [HttpPost]
        public void settingDeleteProfileToCommunication(string keyDialog, int profileId)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.settingDeleteProfileToCommunicationLogic(authorization.getUserId(), keyDialog, profileId);
        }

        [Route("api/Communication/Search")]
        [HttpGet]
        public List<CommunicationDataModel> searchCommunicationByName(string searchName)
        {    
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.searchCommunicationByNameLogic(authorization.getUserId(), searchName);
        }

    }
}