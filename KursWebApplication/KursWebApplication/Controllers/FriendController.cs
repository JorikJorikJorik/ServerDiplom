using System.Web.Http;
using KursWebApplication.Models;
using KursWebApplication.Business_Logic;
using System.Collections.Generic;
using KursWebApplication.Business_Logic.Kurs3._2;

namespace KursWebApplication.Controllers
{
    public class FriendController : ApiController
    {
        FriendLogic logic = new FriendLogic();

        [Route("api/Friend/FullFriends")]
        [HttpGet]
        public FullFriendModel getFullFriends(int profileId, int typeUser)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.getFullFriendLogic(authorization.getUserId(), profileId, typeUser);
        }

        [Route("api/Friend/AllFriends")]
        [HttpGet]
        public List<FriendModel> getAllFriends(string keyDialog, bool searchForCreate)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.getAllFriendsLogic(authorization.getUserId(), keyDialog, searchForCreate);
        }

        [Route("api/Friend/Search")]
        [HttpGet]
        public List<FriendModel> searchFriend(int count, int number, string searchName)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.searchFriendLogic(authorization.getUserId(), count, number, searchName);
        }

        [Route("api/Friend/Add")]
        [HttpPost]
        public void wantToAddFriend(int wantAddProfileId)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.wantToAddFriendLogic(authorization.getUserId(), wantAddProfileId);
        }

        [Route("api/Friend/Confirm")]
        [HttpPost]
        public void confirmFriend(int confirmProfileId, bool addFriend)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.confirmFriendLogic(authorization.getUserId(), confirmProfileId, addFriend);
        }

        [Route("api/Friend/Delete")]
        [HttpPost]
        public void deleteFromFriend(int deleteProfileId)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.deleteFromFriendLogic(authorization.getUserId(), deleteProfileId);
        }
    }
}