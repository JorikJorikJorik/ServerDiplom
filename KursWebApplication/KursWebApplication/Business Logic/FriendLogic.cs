using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;
using KursWebApplication.Models;

namespace KursWebApplication.Business_Logic
{
    public class FriendLogic
    {
        FriendDataAccess dataAccess = new FriendDataAccess();

        public FullFriendModel getFullFriendLogic(int userId, int profileId, int typeUser)
        {
            return dataAccess.getFullFriendModel(userId, profileId, typeUser);
        }

        public List<FriendModel> getAllFriendsLogic(int userId, string keyDialog, bool searchForCreate)
        {
            return dataAccess.getAllFriend(userId, keyDialog, searchForCreate);
        }

        public List<FriendModel> searchFriendLogic(int userId, int count, int number, string searchName)
        {
            return dataAccess.searchFriend(userId, count, number, searchName);
        }

        public void wantToAddFriendLogic(int userId, int wantAddProfileId)
        {
            dataAccess.wantToAddFriend(userId, wantAddProfileId);
        }

        public void confirmFriendLogic(int userId, int confirmProfileId, bool addFriend)
        {
            dataAccess.confirmFriend(userId, confirmProfileId, addFriend);
        }

        public void deleteFromFriendLogic(int userId, int deleteProfileId)
        {
            dataAccess.deleteFromFriend(userId, deleteProfileId);
        }
    }
}