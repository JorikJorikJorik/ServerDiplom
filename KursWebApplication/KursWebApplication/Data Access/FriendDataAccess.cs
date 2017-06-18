using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Business_Logic;
using KursWebApplication.Models;

namespace KursWebApplication.Data_Access
{
    public class FriendDataAccess
    {

        public FullFriendModel getFullFriendModel(int userId, int profileId, int typeUser)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
            var fullFriendModel = new FullFriendModel();
            List<FriendModel> allFirend = new List<FriendModel>();
            List<FriendModel> onlineFirend = new List<FriendModel>();
            List<FriendModel> thirdColumnFirend = new List<FriendModel>();


            var userModel = db.user.Where(u => u.ProfileId == profileId).First();
            var myUserModel = db.user.Where(u => u.UserId == userId).First();
            var myProfileModel = getProfileDbByUserId(db, userId);

            var allFriendIdArray = userModel.FriendIdArray;

            foreach (int friendId in allFriendIdArray)
            {
                var friendProfileModel = db.profile.Where(p => p.ProfileId == friendId).First();
                allFirend.Add(createFriendModelFromDb(friendProfileModel, dateUtils));
            }

            foreach (FriendModel friendProfile in allFirend)
            {
                if (friendProfile.stateLastActivity.Equals("Online")) {
                    onlineFirend.Add(friendProfile);
                }
            }

            if (typeUser > 1)
            {
                if (allFirend.Count() > 0)
                {
                    if (myUserModel.FriendIdArray.Count() > 0)
                    {
                        foreach (int myFriendId in myUserModel.FriendIdArray)
                        {
                            var mutaulFriend = allFirend.Where(f => f.ProfileId == myFriendId);
                            if (mutaulFriend.Count() > 0)
                            {
                                thirdColumnFirend.Add(mutaulFriend.First());
                            }
                        }
                    }
                }
            }
            else
            {
                var confirmPeoplidArray = userModel.FriendPossibleIdArray;
                foreach (int possibleFriendId in confirmPeoplidArray)
                {
                    var friendProfileModel = db.profile.Where(p => p.ProfileId == possibleFriendId).First();
                    thirdColumnFirend.Add(createFriendModelFromDb(friendProfileModel, dateUtils));
                }
            }

            fullFriendModel.TypeUser = typeUser;
            fullFriendModel.AllFirend = allFirend;
            fullFriendModel.OnlineFirend = onlineFirend;
            fullFriendModel.ThirdColumnFirend = thirdColumnFirend;

            return fullFriendModel;
        }

        public List<FriendModel> getAllFriend(int userId, string keyDialog, bool searchForCreate)
        {
            var db = new MyDBModels.DB();
            var userModel = db.user.Where(u => u.UserId == userId).First();
            var dateUtils = new DateUtils();
            var allFriendsList = new List<FriendModel>();

            var allFriendIdArray = userModel.FriendIdArray.ToList();
            if (!searchForCreate)
            {
                db.communication.Where(c => c.KeyDialog == keyDialog).First().ParticipantProfileIdArray.ToList().ForEach(delegate (int profileId)
                 {
                     allFriendIdArray.Remove(profileId);
                 });
            }
            foreach (int friendId in allFriendIdArray)
            {
                var friendProfileModel = db.profile.Where(p => p.ProfileId == friendId).First();
                allFriendsList.Add(createFriendModelFromDb(friendProfileModel, dateUtils));
            }

            return allFriendsList;
        }

        public List<FriendModel> searchFriend(int userId, int count, int number, string searchName)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
            var myUser = db.user.Where(u => u.UserId == userId).First();
            var friendProfileIdarray = myUser.FriendIdArray;

            var searchListFriend = new List<FriendModel>();
            var listProfileModel = db.profile.ToList().OrderBy(p => p.Name);
            var withoutFriendList = listProfileModel.ToList();
            var searchListFriendDb = new List<MyDBModels.Profile>();
            var parseListFriendDb = new List<MyDBModels.Profile>();
            
            friendProfileIdarray.ToList().ForEach(delegate (int friendId)
            {
                var friendModel = db.profile.Where(p => p.ProfileId == friendId).First();
                withoutFriendList.Remove(friendModel);
            });

            var myProfile = db.profile.Where(p => p.ProfileId == myUser.ProfileId).First();
            withoutFriendList.Remove(myProfile);

            foreach (MyDBModels.Profile profile in withoutFriendList)
            {
                var fullName = String.Format("{0} {1}", profile.Name, profile.LastName);
                if (fullName.Contains(searchName))
                {
                    parseListFriendDb.Add(profile);
                }
            }

            var countProfileDb = parseListFriendDb.Count();
            for (int i = (number - 1) * count; i < count * number; i++)
            {
                if (i < countProfileDb)
                {
                    searchListFriendDb.Add(parseListFriendDb.ElementAt(i));
                }
            }

            foreach (MyDBModels.Profile profile in searchListFriendDb)
            {
                searchListFriend.Add(createFriendModelFromDb(profile, dateUtils));
            }

            return searchListFriend;
        }

        public void wantToAddFriend(int userId, int wantAddProfileId)
        {
            var db = new MyDBModels.DB();
            var userModel = db.user.Where(u => userId == userId).First();
            var myProfileModel = getProfileDbByUserId(db, userId);
            var userModelWantAddFriend = db.user.Where(u => u.ProfileId == wantAddProfileId).First();

            var checkAddToThisUserAlready = userModelWantAddFriend.FriendPossibleIdArray.Where(p => p == myProfileModel.ProfileId).Count() > 0;

            if (!checkAddToThisUserAlready)
            {
                var friendPossibleArrayId = userModelWantAddFriend.FriendPossibleIdArray.ToList();
                friendPossibleArrayId.Add(myProfileModel.ProfileId);
                userModelWantAddFriend.FriendPossibleIdArray = friendPossibleArrayId.ToArray();
            }

            db.SaveChanges();

           // var dataAccess = new FirebaseDataAccess();
           // dataAccess.sendRequestToFirebase(, 1, userModel);
        }

        public void confirmFriend(int userId, int confirmProfileId, bool addFriend)
        {
            var db = new MyDBModels.DB();
            var myUserModel = db.user.Where(u => u.UserId == userId).First();
            var myProfileModel = db.profile.Where(p => p.ProfileId == myUserModel.ProfileId).First();
            var friendUserModel = db.user.Where(u => u.ProfileId == confirmProfileId).First();

            var possibleProfileIdArray = myUserModel.FriendPossibleIdArray;

            if (addFriend) {

                var friendArrayIdUpdate = myUserModel.FriendIdArray.ToList();
                friendArrayIdUpdate.Add(confirmProfileId);
                myUserModel.FriendIdArray = friendArrayIdUpdate.ToArray();

                var myFirendFriendArrayIdUpdate = friendUserModel.FriendIdArray.ToList();
                myFirendFriendArrayIdUpdate.Add(myUserModel.ProfileId);
                friendUserModel.FriendIdArray = myFirendFriendArrayIdUpdate.ToArray();

                if (friendUserModel.FriendPossibleIdArray.Contains(myUserModel.ProfileId))
                {
                    var firendPossibleArrayIdUpdate = friendUserModel.FriendPossibleIdArray.ToList();
                    firendPossibleArrayIdUpdate.Remove(myUserModel.ProfileId);
                    myUserModel.FriendPossibleIdArray = firendPossibleArrayIdUpdate.ToArray();
                }

                CommunicationDataAccess dataAccess = new CommunicationDataAccess();

                if (!dataAccess.searchAndConnectOldCommunication(myProfileModel.ProfileId, friendUserModel.ProfileId))
                {
                    CommunicationShortModel shortModel = new CommunicationShortModel();
                    var firendProfile = db.profile.Where(fp => fp.ProfileId == friendUserModel.ProfileId).First();

                    shortModel.Name = String.Format("{0} {1} - {2} {3}", firendProfile.Name, firendProfile.LastName, myProfileModel.Name, myProfileModel.LastName);
                    shortModel.PhotoUrl = firendProfile.PhotoUrl;
                    shortModel.ParticipantProfileIdArray = new int[] { firendProfile.ProfileId, myUserModel.ProfileId };

                    dataAccess.createCommunication(userId, shortModel);

                    db.SaveChanges();
                }
            }

            var possibleArrayIdUpdate = possibleProfileIdArray.ToList();
            possibleArrayIdUpdate.Remove(confirmProfileId);
            myUserModel.FriendPossibleIdArray = possibleArrayIdUpdate.ToArray();
          
            db.SaveChanges();
        }

        public void deleteFromFriend(int userId, int deleteProfileId)
        {
            var db = new MyDBModels.DB();
            var myProfileModel = getProfileDbByUserId(db, userId);
            var myUserModel = db.user.Where(u => u.UserId == userId).First();
            var deleteUserModel = db.user.Where(u => u.ProfileId == deleteProfileId).First();

            deleteFriendFromDb(db, myUserModel, deleteProfileId);
            deleteFriendFromDb(db, deleteUserModel, myProfileModel.ProfileId);

        }

        private void deleteFriendFromDb(MyDBModels.DB db, MyDBModels.User fromDeleteUserModel, int deleteProfileId)
        {
            var friendArrayIdUpdate = fromDeleteUserModel.FriendIdArray.ToList();
            friendArrayIdUpdate.Remove(deleteProfileId);
            fromDeleteUserModel.FriendIdArray = friendArrayIdUpdate.ToArray();

            var dataAccess = new CommunicationDataAccess();
            int communicationId = dataAccess.searchCommunication(fromDeleteUserModel.ProfileId, deleteProfileId);

            var communicationIdArray = fromDeleteUserModel.CommunicationIdArray;
            var communicationIdList = communicationIdArray.ToList();
            communicationIdList.Remove(communicationId);
            fromDeleteUserModel.CommunicationIdArray = communicationIdList.ToArray();

            if (fromDeleteUserModel.CommunicationPinIdArray.Contains(communicationId))
            {
                var communicationPinIdArray = fromDeleteUserModel.CommunicationPinIdArray;
                var communicationPinIdList = communicationPinIdArray.ToList();
                communicationPinIdList.Remove(communicationId);
                fromDeleteUserModel.CommunicationPinIdArray = communicationPinIdList.ToArray();
            }

            db.SaveChanges();
        }

        private FriendModel createFriendModelFromDb(MyDBModels.Profile profile, DateUtils utils)
        {
            FriendModel model = new FriendModel();

            model.ProfileId = profile.ProfileId;
            model.FullName = String.Format("{0} {1}", profile.Name, profile.LastName);
            model.PhotoUrl = profile.PhotoUrl;
            model.City = profile.City;
            model.stateLastActivity = profile.TimeLastActive;//utils.calculateStateLastActivity(profile.TimeLastActive);

            return model;
        }

        private MyDBModels.Profile getProfileDbByUserId(MyDBModels.DB db, int userId)
        {
            var userModel = db.user.Where(u => u.UserId == userId).First();
            return db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();
        }

    }
}