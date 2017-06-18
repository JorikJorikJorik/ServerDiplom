using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;
using KursWebApplication.Models;

namespace KursWebApplication.Business_Logic
{
    public class CommunicationLogic
    {
        CommunicationDataAccess dataAccess = new CommunicationDataAccess();

        public List<CommunicationDataModel> getCommunicationListByPartLogic(int userId, int count, int number)
        {
           return dataAccess.getCommunicationListByPart(userId, count, number);
        }

        public void createCommunicationLogic(int userId, CommunicationShortModel model)
        {
            dataAccess.createCommunication(userId, model);
        }

        public void deleteCommunicationLogic(int userId, string keyDialog)
        {
            dataAccess.deleteCommunication(userId, keyDialog);
        }

        public void leaveCommunicationLogic(int userId, string keyDialog)
        {
            dataAccess.leaveCommunication(userId, keyDialog);
        }

        public void clearHistoryLogic(int userId, string keyDialog)
        {
            dataAccess.clearHistory(userId, keyDialog);
        }

        public void pinToTopLogic(int userId, string keyDialog, bool isPin)
        {
            dataAccess.pinToTop(userId, keyDialog, isPin);
        }

        public void settingChangeInfoCommunicationLogic(int userId, string keyDialog, CommunicationChangeModel model)
        {
            dataAccess.settingChangeInfoCommunication(userId, keyDialog, model);
        }

        public void settingAddProfileToCommunicationLogic(int userId, string keyDialog, int profileId)
        {
            dataAccess.settingAddProfileToCommunication(userId, keyDialog, profileId);
        }

        public void settingDeleteProfileToCommunicationLogic(int userId, string keyDialog, int profileId)
        {
            dataAccess.settingDeleteProfileToCommunication(userId, keyDialog, profileId);
        }

        public List<CommunicationDataModel> searchCommunicationByNameLogic(int userId, string searchElement)
        {
            return dataAccess.searchCommunicationByName(userId, searchElement);
        }

    }
}