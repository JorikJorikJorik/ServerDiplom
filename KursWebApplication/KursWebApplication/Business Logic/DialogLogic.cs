using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;
using KursWebApplication.Models;

namespace KursWebApplication.Business_Logic
{
    public class DialogLogic
    {
        DialogDataAccess dataAccess = new DialogDataAccess();

        public DialogModel getMessageListByPartLogic(int userId, int count, int number, string keyDialog)
        {
            return dataAccess.getMessageListByPart(userId, count, number, keyDialog);
        }

        public void sendMessageLogic(int userId, MessageSendModel model, string keyDialog)
        {
            dataAccess.sendMessage(userId, model, keyDialog);
        }

        public void deleteMessageLogic(int userId, MessageDeleteListModel model)
        {
            dataAccess.deleteMessage(userId, model);
        }

        public void isReadMessageLogic(int userId, string keyDialog)
        {
            dataAccess.isReadMessage(userId, keyDialog);
        }

    }
}