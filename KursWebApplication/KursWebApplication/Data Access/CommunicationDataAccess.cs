using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using KursWebApplication.Business_Logic;
using KursWebApplication.Models;

namespace KursWebApplication.Data_Access
{
    public class CommunicationDataAccess
    {

        public List<CommunicationDataModel> getCommunicationListByPart(int userId, int count, int number)
        {
            var db = new MyDBModels.DB();
            var user = db.user.Where(u => u.UserId == userId).First();
            var myProfileId = user.ProfileId;
            var myCommunicationListDB = new List<MyDBModels.Communication>();
            var communicationDataModelList = new List<CommunicationDataModel>();
            var partCommunicationListDb = new List<MyDBModels.Communication>();

            user.CommunicationIdArray.ToList().ForEach(delegate (int communicationId)
            {
                myCommunicationListDB.Add(db.communication.Where(c => c.CommunicationId == communicationId).First());
            });

            var countProfileDb = myCommunicationListDB.Count();   
            for (int i = (number - 1) * count; i < count * number; i++)
            {
                if (i < countProfileDb)
                {
                    partCommunicationListDb.Add(myCommunicationListDB.ElementAt(i));
                }
            }

            foreach(MyDBModels.Communication communication in partCommunicationListDb)
            {
                communicationDataModelList.Add(createCommunicationDataFromDb(communication, myProfileId, 1));
            }

            return communicationDataModelList;
        }

        public void createCommunication(int userId, CommunicationShortModel model)
        {
            var db = new MyDBModels.DB();
            var keyDailog = generateKeyDialog();
            var dateUtils = new DateUtils();
            var userModel = db.user.Where(u => u.UserId == userId).First();
            MyDBModels.Communication communication = new MyDBModels.Communication();
           
            communication.KeyDialog = keyDailog;
            communication.Name = model.Name;
            communication.PhotoUrl = model.PhotoUrl;
            communication.ParticipantProfileIdArray = model.ParticipantProfileIdArray;
            communication.CreaterProfileId = userModel.ProfileId;
            communication.AddProfileTimestampArray = Enumerable.Repeat(dateUtils.DateTimeToUnixTimeStamp(DateTime.Now), model.ParticipantProfileIdArray.Count()).ToArray();
           
            if (model.ParticipantProfileIdArray.Count() > 2)
            {
                communication.IsGroup = true;
            }

            else if (model.ParticipantProfileIdArray.Count() < 3) {
                foreach(int profileId in model.ParticipantProfileIdArray)
                {
                    var profile = db.profile.Where(p => p.ProfileId == profileId).First();
                    var searchFullName = String.Format("{0} {1}", profile.Name, profile.LastName);
                    if (model.Name.Contains(searchFullName))
                    {
                        communication.IsGroup = false;
                        break;
                    }
                    else communication.IsGroup = true;
                };
            }
            else communication.IsGroup = false;

            var systemMessageCreateId = createSystemMessage(userId, String.Format("{0} {1}", "Created communication", model.Name), 2);
            communication.MessageIdArray = new int[] { systemMessageCreateId };

            db.communication.Add(communication);
            db.SaveChanges();

            int communicationId = db.communication.Where(c => c.KeyDialog == keyDailog).First().CommunicationId;   
            model.ParticipantProfileIdArray.ToList().ForEach(delegate (int profileId)
            {
                addCommunicationToUserByProfile(db, profileId, communicationId);
            });
        }
       
        public void deleteCommunication(int userId, string keyDialog)
        {
            var db = new MyDBModels.DB();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            var userModel = db.user.Where(u => u.UserId == userId).First();
            var profileId = userModel.ProfileId;
            var dateUtils = new DateUtils();
            //  removeCommunication(db, communicationModel, userModel.ProfileId);

            int indexProfile = Array.IndexOf(communicationModel.ParticipantProfileIdArray, profileId);

            var addProfileTimestampList = communicationModel.AddProfileTimestampArray.ToList();
            var timestamp = addProfileTimestampList.ElementAt(indexProfile);
            addProfileTimestampList.Remove(timestamp);
            addProfileTimestampList.Insert(indexProfile, dateUtils.DateTimeToUnixTimeStamp(DateTime.Now));
            communicationModel.AddProfileTimestampArray = addProfileTimestampList.ToArray();
            db.SaveChanges();

            var communicationIdArray = userModel.CommunicationIdArray;
            var communicationIdList = communicationIdArray.ToList();
            communicationIdList.Remove(communicationModel.CommunicationId);
            userModel.CommunicationIdArray = communicationIdList.ToArray();

            if (db.user.Where(u => u.ProfileId == profileId).First().CommunicationPinIdArray.Contains(communicationModel.CommunicationId))
            {
                var communicationPinIdArray = userModel.CommunicationPinIdArray;
                var communicationPinIdList = communicationPinIdArray.ToList();
                communicationPinIdList.Remove(communicationModel.CommunicationId);
                userModel.CommunicationPinIdArray = communicationPinIdList.ToArray();
            }

            db.SaveChanges();

        }

        public void leaveCommunication(int userId, string keyDialog)
        {
            var db = new MyDBModels.DB();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            var userModel = db.user.Where(u => u.UserId == userId).First();

            removeCommunication(db, communicationModel, userModel.ProfileId);

            var nameAddedProfile = db.profile.Where(p => p.ProfileId == userModel.ProfileId).First().Name;
            var messageAddId = createSystemMessage(userId, String.Format("{0} {1}", "Leave user", nameAddedProfile), 2);

            var messageIdList = communicationModel.MessageIdArray.ToList();
            messageIdList.Add(messageAddId);
            communicationModel.MessageIdArray = messageIdList.ToArray();

            db.SaveChanges();
        }

        public void clearHistory(int userId, string keyDialog)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            int indexProfile = Array.IndexOf(communicationModel.ParticipantProfileIdArray, db.user.Where(u => u.UserId == userId).First().ProfileId);
            var addProfileTimestampList = communicationModel.AddProfileTimestampArray.ToList();
            var timestamp = addProfileTimestampList.ElementAt(indexProfile);
            addProfileTimestampList.Remove(timestamp);
            addProfileTimestampList.Insert(indexProfile, dateUtils.DateTimeToUnixTimeStamp(DateTime.Now));
            communicationModel.AddProfileTimestampArray = addProfileTimestampList.ToArray();
            db.SaveChanges();

            var messageAddId = createSystemMessage(userId, "History was cleared", 3);
            var messageIdList = communicationModel.MessageIdArray.ToList();
            messageIdList.Add(messageAddId);
            communicationModel.MessageIdArray = messageIdList.ToArray();

            db.SaveChanges();
        }

        public void pinToTop(int userId, string keyDialog, bool isPin)
        {
            var db = new MyDBModels.DB();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            var userModel = db.user.Where(u => u.UserId == userId).First();

            var communicationPinIdArray = userModel.CommunicationPinIdArray;
            var communicationPinIdList = communicationPinIdArray.ToList();
            if (isPin)
            {
                communicationPinIdList.Add(communicationModel.CommunicationId);
            }
            else
            {
                communicationPinIdList.Remove(communicationModel.CommunicationId);
            }
            userModel.CommunicationPinIdArray = communicationPinIdList.ToArray();

            db.SaveChanges();
        }

        public void settingChangeInfoCommunication(int userId, string keyDialog, CommunicationChangeModel model)
        {
            var db = new MyDBModels.DB();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();

            communicationModel.Name = model.Name;
            communicationModel.PhotoUrl = model.Photourl;

            db.SaveChanges();
        }

        public void settingAddProfileToCommunication(int userId, string keyDialog, int profileId)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();

            var participantProfileIdList = communicationModel.ParticipantProfileIdArray.ToList();
            participantProfileIdList.Add(profileId);
            communicationModel.ParticipantProfileIdArray = participantProfileIdList.ToArray();

            var addProfileTimestampList = communicationModel.AddProfileTimestampArray.ToList();
            addProfileTimestampList.Add(dateUtils.DateTimeToUnixTimeStamp(DateTime.Now));
            communicationModel.AddProfileTimestampArray = addProfileTimestampList.ToArray();

            var communicationIdArray = db.user.Where(u => u.ProfileId == profileId).First().CommunicationIdArray;
            var communicationIdList = communicationIdArray.ToList();
            communicationIdList.Add(communicationModel.CommunicationId);
            db.user.Where(u => u.ProfileId == profileId).First().CommunicationIdArray = communicationIdList.ToArray();

            db.SaveChanges();

            var nameAddedProfile = db.profile.Where(p => p.ProfileId == profileId).First().Name;
            var messageAddId = createSystemMessage(userId, String.Format("{0} {1}", "Added user", nameAddedProfile), 2);

            var messageIdList = communicationModel.MessageIdArray.ToList();
            messageIdList.Add(messageAddId);
            communicationModel.MessageIdArray = messageIdList.ToArray();

            db.SaveChanges();
        }

        public void settingDeleteProfileToCommunication(int userId, string keyDialog, int profileId)
        {
            var db = new MyDBModels.DB();
            var communicationModel = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            var myUserModel = db.user.Where(u => u.UserId == userId).First();
            if (myUserModel.ProfileId == communicationModel.CreaterProfileId)
            {
                removeCommunication(db, communicationModel, profileId);
            }

            var nameAddedProfile = db.profile.Where(p => p.ProfileId == profileId).First().Name;
            var messageAddId = createSystemMessage(userId, String.Format("{0} {1}", "Delete user", nameAddedProfile), 2);

            var messageIdList = communicationModel.MessageIdArray.ToList();
            messageIdList.Add(messageAddId);
            communicationModel.MessageIdArray = messageIdList.ToArray();

            db.SaveChanges();

        }

        public List<CommunicationDataModel> searchCommunicationByName(int userId, string searchElement)
        {
            var db = new MyDBModels.DB();
            var user = db.user.Where(u => u.UserId == userId).First();
            var myProfileId = user.ProfileId;
            var myCommunicationListDB = new List<MyDBModels.Communication>();
            var communicationDataModelList = new List<CommunicationDataModel>();

            if (searchElement.Count() > 0)
            {
                user.CommunicationIdArray.ToList().ForEach(delegate (int communicationId)
                {
                    myCommunicationListDB.Add(db.communication.Where(c => c.CommunicationId == communicationId).First());
                });

                var searchCommunicationListDb = myCommunicationListDB.FindAll(c => c.Name.Contains(searchElement));

                foreach (MyDBModels.Communication communication in searchCommunicationListDb)
                {
                    communicationDataModelList.Add(createCommunicationDataFromDb(communication, myProfileId, 1));
                }
            }
            return communicationDataModelList;
        }

        private void removeCommunication(MyDBModels.DB db, MyDBModels.Communication communicationModel, int profileId)
        {
            var dateUtils = new DateUtils();
            int indexRemoveProfile = Array.IndexOf(communicationModel.ParticipantProfileIdArray, profileId);
            
            var participantProfileIdList = communicationModel.ParticipantProfileIdArray.ToList();
            participantProfileIdList.Remove(profileId);
            communicationModel.ParticipantProfileIdArray = participantProfileIdList.ToArray();

            var addProfileTimestampList = communicationModel.AddProfileTimestampArray.ToList();
            addProfileTimestampList.Remove(communicationModel.AddProfileTimestampArray.ElementAt(indexRemoveProfile));
            communicationModel.AddProfileTimestampArray = addProfileTimestampList.ToArray();

            var userModel = db.user.Where(u => u.ProfileId == profileId).First();
            var communicationIdArray = userModel.CommunicationIdArray;
            var communicationIdList = communicationIdArray.ToList();
            communicationIdList.Remove(communicationModel.CommunicationId);
            userModel.CommunicationIdArray = communicationIdList.ToArray();

            if (db.user.Where(u => u.ProfileId == profileId).First().CommunicationPinIdArray.Contains(communicationModel.CommunicationId))
            {
                var communicationPinIdArray = userModel.CommunicationPinIdArray;
                var communicationPinIdList = communicationPinIdArray.ToList();
                communicationPinIdList.Remove(communicationModel.CommunicationId);
                userModel.CommunicationPinIdArray = communicationPinIdList.ToArray();
            }

            db.SaveChanges();
        }

        public void addCommunicationToUserByProfile(MyDBModels.DB db, int profileId, int communicationId)
        {
            var userModel = db.user.Where(u => u.ProfileId == profileId).First();
            var communicationArray = userModel.CommunicationIdArray.ToList();
            communicationArray.Add(communicationId);
            userModel.CommunicationIdArray = communicationArray.ToArray();

            db.SaveChanges();
        }

        private CommunicationDataModel createCommunicationDataFromDb(MyDBModels.Communication modelDb, int myProfileId, int countEnd)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
           
            CommunicationDataModel model = new CommunicationDataModel();
            model.CommunicationId = modelDb.CommunicationId;
            model.Name = modelDb.Name;
            model.PhotoUrl = modelDb.PhotoUrl;
            model.IsGroup = modelDb.IsGroup;

            if (modelDb.MessageIdArray.Count() > 0)
            {
                var lastMessageId = modelDb.MessageIdArray.ElementAt(modelDb.MessageIdArray.Count() - countEnd);
                var lastMessageModel = db.message.Where(m => m.MessageId == lastMessageId).First();

                if (lastMessageModel.TypeMessage == 1)
                {
                    model.LastWrittenName = lastMessageModel.ProfileId == myProfileId ? "You" : modelDb.IsGroup ? db.profile.Where(p => p.ProfileId == lastMessageModel.ProfileId).First().Name : "";
                    model.LastMessage = lastMessageModel.DataMessage;
                    model.LastDate = lastMessageModel.TimeWritten;
                    model.IsRead = lastMessageModel.ProfileId == myProfileId ? lastMessageModel.IsReadProfileId.Count() > 1 : true;
                    model.CountUnreadMessage = calculateCountUnreadMessages(modelDb.MessageIdArray, myProfileId);
                }

                else if (lastMessageModel.TypeMessage == 2)
                {
                    model.LastWrittenName = "";
                    model.LastMessage = lastMessageModel.DataMessage;
                    model.LastDate = lastMessageModel.TimeWritten;
                    model.IsRead = lastMessageModel.ProfileId == myProfileId ? lastMessageModel.IsReadProfileId.Count() > 1 : true;
                    model.CountUnreadMessage = calculateCountUnreadMessages(modelDb.MessageIdArray, myProfileId);
                }

                else if (lastMessageModel.TypeMessage == 3)
                {
                    if (lastMessageModel.ProfileId == myProfileId)
                    {
                        model.LastWrittenName = "";
                        model.LastMessage = lastMessageModel.DataMessage;
                        model.LastDate = lastMessageModel.TimeWritten;
                        model.IsRead = true;
                        model.CountUnreadMessage = 0;
                    }
                    else {
                        model = createCommunicationDataFromDb(modelDb, myProfileId, 2);
                    }
                }
            }

            return model;
        }


        public int calculateCountUnreadMessages(int [] messageIdArray, int myProfileId)
        {
            var db = new MyDBModels.DB();
            int countMessage = 0;

            if (messageIdArray.Count() > 0)
            {
                messageIdArray.ToList().ForEach(delegate (int messageId)
                {
                    var messageModel = db.message.Where(m => m.MessageId == messageId).First();
                    if (!messageModel.IsReadProfileId.Contains(myProfileId)) countMessage++;
                });
            }
            return countMessage;
        }

        public bool searchAndConnectOldCommunication(int myProfileId, int friendProfileId)
        {
            int communicationId = searchCommunication(myProfileId, friendProfileId);
            if (communicationId > 0)
            {
                connectOldCommunicationToUser(myProfileId, communicationId);
                connectOldCommunicationToUser(friendProfileId, communicationId);
                return true;
            }
            return false;
        }

        public int searchCommunication(int myProfileId, int friendProfileId)
        {
            var db = new MyDBModels.DB();
            var communicationDB = db.communication;
            var communicationId = 0;

            foreach (MyDBModels.Communication communication in communicationDB)
            {
                if (!communication.IsGroup && communication.ParticipantProfileIdArray.Count() == 2 && communication.ParticipantProfileIdArray.Contains(myProfileId) && communication.ParticipantProfileIdArray.Contains(friendProfileId))
                    communicationId = communication.CommunicationId;
            };
            return communicationId;
        }

        public void connectOldCommunicationToUser(int profileId, int communicationId)
        {
            var db = new MyDBModels.DB();

            var userModel = db.user.Where(u => u.ProfileId == profileId).First();
            if (!userModel.CommunicationIdArray.Contains(communicationId))
            {
                var userCommunicationList = userModel.CommunicationIdArray.ToList();
                userCommunicationList.Add(communicationId);
                userModel.CommunicationIdArray = userCommunicationList.ToArray();

                db.SaveChanges();
            }
        }

        public string generateKeyDialog()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 16)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int createSystemMessage(int userId, string dateMessage, int type)
        {
            var db = new MyDBModels.DB();
            var user = db.user.Where(u => u.UserId == userId).First();
            var profileId = user.ProfileId;

            var dateCreate = DateTime.Now;

            MyDBModels.Message messsage = new MyDBModels.Message();
            messsage.DataMessage = dateMessage;
            messsage.TimeWritten = dateCreate;
            messsage.IsReadProfileId = new int[] { profileId };
            messsage.ProfileId = profileId;
            messsage.TypeMessage = type;
            db.message.Add(messsage);
            db.SaveChanges();

            return db.message.Where(m => m.ProfileId == profileId && m.TimeWritten == messsage.TimeWritten).FirstOrDefault().MessageId;
        }
    }
}