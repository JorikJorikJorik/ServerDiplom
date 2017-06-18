using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Business_Logic;
using KursWebApplication.Models;

namespace KursWebApplication.Data_Access
{
    public class DialogDataAccess
    {
        

        public DialogModel getMessageListByPart(int userId, int count, int number, string keyDialog)
        {
            //get message by part + get info about communicatin + full profile info list (вызывать запрос при входе в диалог)
            //read

            var db = new MyDBModels.DB();
            var user = db.user.Where(u => u.UserId == userId).First();
            var myProfileId = user.ProfileId;
            var currentCommunication = db.communication.Where(c => c.KeyDialog == keyDialog).First();

            DialogModel dialogDodel = new DialogModel();
            if (number == 1)
                dialogDodel.DialogInfo = createInfoAboutUserCommunication(currentCommunication, myProfileId);
            dialogDodel.Messages = createMessageList(db, count, number, keyDialog, userId);

            return dialogDodel;

        }

        public void sendMessage(int userId, MessageSendModel model, string keyDialog)
        {
            var db = new MyDBModels.DB();
            var user = db.user.Where(u => u.UserId == userId).First();
            var profileId = user.ProfileId;

            var currentCommunication = db.communication.Where(c => c.KeyDialog == keyDialog).First();

            MyDBModels.Message messsage = new MyDBModels.Message();
            messsage.DataMessage = model.DataMessage;
            messsage.TimeWritten = model.TimeWritten;
            messsage.IsReadProfileId = new int[] { profileId };
            messsage.ProfileId = profileId;
            messsage.TypeMessage = 1;
            db.message.Add(messsage);
            db.SaveChanges();

            var messageIdArray = currentCommunication.MessageIdArray.ToList();
            messageIdArray.Add(db.message.Where(m => m.ProfileId == profileId && m.TimeWritten == model.TimeWritten).FirstOrDefault().MessageId);//OrderByDescending need?
            currentCommunication.MessageIdArray = messageIdArray.ToArray();

            var dataAccess = new CommunicationDataAccess();

            if (!currentCommunication.IsGroup)
            {
                var friendProfileId = currentCommunication.ParticipantProfileIdArray.Where(p => p != profileId).First();
                dataAccess.searchAndConnectOldCommunication(profileId, friendProfileId);
            }

            db.SaveChanges();

        }

        public void isReadMessage(int userId, string keyDialog)
        {
            var db = new MyDBModels.DB();
            var user = db.user.Where(u => u.UserId == userId).First();
            var profileId = user.ProfileId;
            var currentCommunication = db.communication.Where(c => c.KeyDialog == keyDialog).First();

            currentCommunication.MessageIdArray.ToList().ForEach(delegate(int messageId)
            {
                var message = db.message.Where(m => m.MessageId == messageId).First();
                if (!message.IsReadProfileId.Contains(profileId))
                {
                    changeIsReadMessage(message, profileId, messageId);
                    db.SaveChanges();
                }
            });       
        }

        public void deleteMessage(int userId, MessageDeleteListModel model)
        {
            var db = new MyDBModels.DB();

            foreach (int messageDeletetId in model.MessageIdArray)
            {
                deleteMessageItem(messageDeletetId, model.KeyDialog);
            }
        }

        private void deleteMessageItem(int messageId, string keyDialog)
        {
            var db = new MyDBModels.DB();
            var messageDelete = db.message.Where(m => m.MessageId == messageId).First();

            var currentCommunication = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            var messageIdArray = currentCommunication.MessageIdArray.ToList();
            messageIdArray.Remove(messageId);
            currentCommunication.MessageIdArray = messageIdArray.ToArray();

            db.message.Remove(messageDelete);
            db.SaveChanges();
        }

        private DialogInfoModel createInfoAboutUserCommunication(MyDBModels.Communication currentCommunication, int myProfileId)
        {
            var participantProfileId = currentCommunication.ParticipantProfileIdArray;

            DialogInfoModel dialogInfoModel = new DialogInfoModel();
            dialogInfoModel.FullName = currentCommunication.Name;
            dialogInfoModel.PhotoUrl = currentCommunication.PhotoUrl;

            var participantCount = participantProfileId.Count();
            if (currentCommunication.IsGroup)
            {
                dialogInfoModel.SecondInfoCount = String.Format("{0} {1}", participantCount, "members");
                dialogInfoModel.ProfileShortModelList = createProfileShortModelList(participantProfileId, currentCommunication.CreaterProfileId);
            }
            else
            {
                var dateUtils = new DateUtils();
                var profileFriend = participantProfileId.Where(p => p != myProfileId).First();

                var db = new MyDBModels.DB();
                var profileModel = db.profile.Where(p => p.ProfileId == profileFriend).First();
                dialogInfoModel.SecondInfoTime = profileModel.TimeLastActive;// dateUtils.stateLastActivityProfile(profileFriend);
                dialogInfoModel.ProfileId = profileFriend;
            }

            return dialogInfoModel;
        }

        private List<ProfileShortModel> createProfileShortModelList(int[] profileIdArray, int createrProfileId)
        {
            List<ProfileShortModel> profileShortModelList = new List<ProfileShortModel>();
            var dateUtils = new DateUtils();

            profileIdArray.ToList().ForEach(delegate(int profileId)
            {         
                profileShortModelList.Add(createrofileShortModel(profileId, dateUtils, createrProfileId));
            });

            return profileShortModelList;
        }

        private ProfileShortModel createrofileShortModel(int profileId, DateUtils dateUtils, int createrProfileId)
        {
            var db = new MyDBModels.DB();
            var profileModel = getProfileModel(profileId);

            ProfileShortModel profileShortModel = new ProfileShortModel();
            profileShortModel.ProfieId = profileModel.ProfileId;
            profileShortModel.FullName = String.Format("{0} {1}", profileModel.Name, profileModel.LastName);
            profileShortModel.PhotoUrl = profileModel.PhotoUrl;
            profileShortModel.LastActive = profileModel.TimeLastActive;//dateUtils.calculateStateLastActivity(profileModel.TimeLastActive);
            profileShortModel.Creater = profileModel.ProfileId == createrProfileId;

            return profileShortModel;
        }

        private List<MessageModel> createMessageList(MyDBModels.DB db, int count, int number, string keyDialog, int userId)
        {
            var messageList = new List<MessageModel>();
            var listMessageModel = new List<MyDBModels.Message>();
            var dateUtils = new DateUtils();
            var currentCommunication = db.communication.Where(c => c.KeyDialog == keyDialog).First();
            int indexProfile = Array.IndexOf(currentCommunication.ParticipantProfileIdArray, db.user.Where(u => u.UserId == userId).First().ProfileId);
            int limitTimestamp = currentCommunication.AddProfileTimestampArray.ElementAt(indexProfile);

            currentCommunication.MessageIdArray.ToList().ForEach(delegate (int messageId) 
            {
                var messageDb = db.message.Where(m => m.MessageId == messageId).First();
                if (dateUtils.DateTimeToUnixTimeStamp(messageDb.TimeWritten) > limitTimestamp)
                    listMessageModel.Add(messageDb);
            });

            var countMessageDb = listMessageModel.Count();
            List<MyDBModels.Message> messageListDb = new List<MyDBModels.Message>();

            for (int i = (number - 1) * count; i < count * number; i++)
            {
                if (i < countMessageDb)
                {
                    messageListDb.Add(listMessageModel.ElementAt(i));
                }
            }

            foreach (MyDBModels.Message message in messageListDb)
            {
                var messageModel = createMessageModelFromDb(message, userId);
                if(messageModel.TypeMessage < 3)
                    messageList.Add(messageModel);
            }

            return messageList;
        }

        private MessageModel createMessageModelFromDb(MyDBModels.Message message, int userId)
        {
            MessageModel model = new MessageModel();

            var profileModel = getProfileModel(message.ProfileId);

            model.ProfileId = profileModel.ProfileId;
            model.FullName = String.Format("{0} {1}", profileModel.Name, profileModel.LastName);
            model.PhotoUrl = profileModel.PhotoUrl;
            model.DataMessage = message.DataMessage;
            model.isRead = chaeckIsReadMessage(message, userId);
            model.TypeMessage = message.TypeMessage;
            model.TimeWritten = message.TimeWritten;

            return model;
        }

        private void changeIsReadMessage(MyDBModels.Message message, int profileId, int messageId)
        {
            var isReadProfileIdArray = message.IsReadProfileId.ToList();
            isReadProfileIdArray.Add(profileId);
            message.IsReadProfileId = isReadProfileIdArray.ToArray();
        }

        private bool chaeckIsReadMessage(MyDBModels.Message message, int userId)
        {
            var db = new MyDBModels.DB();
            var myProfileId = db.user.Where(u => u.UserId == userId).First().ProfileId;
            return !(message.ProfileId == myProfileId && message.IsReadProfileId.Count() < 2);
        }

        private MyDBModels.Profile getProfileModel(int profileId)
        {
            var db = new MyDBModels.DB();
           return db.profile.Where(p => p.ProfileId == profileId).First();

        }
    }
}