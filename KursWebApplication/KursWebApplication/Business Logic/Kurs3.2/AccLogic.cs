using KursWebApplication.Data_Access;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class AccLogic
    {
        AccDataAccess dataAccess = new AccDataAccess();

        public int logicMethodForPostDataDriver(Models.DriverAccountModel data)
        {
            Models.DriverAccountModel model = new Models.DriverAccountModel();
            model.DriverModel = data.DriverModel;
            Models.AccountModel encrypt = encryptData(data.AccountModel);
            model.AccountModel = encrypt;
            return dataAccess.postDriver(model);
        }

        public int logicMethodForPostDataDispatcher(Models.DispatcherAccountModel data)
        {
            Models.AccountModel encrypt = encryptData(data.AccountModel);
            Models.DispatcherAccountModel model = new Models.DispatcherAccountModel();
            model.AccountModel = encrypt;
            return dataAccess.postDispatcher(model);
             
        }

        public List<string> logicMethodForPostSingDataDispatcher(Models.AccountModel data)
        {
            return dataAccess.postSignIn(data);
        }


        private Models.AccountModel encryptData(AccountModel data) {
            AccountModel account = new AccountModel();
            account.Secondname = EncryptClass.DESEncrypt(data.Secondname);
            account.Password = EncryptClass.MD5Hash(data.Password);
            account.Role = EncryptClass.DESEncrypt(data.Role);
            account.Number = "5HJnFnTWIgk=";
            account.Token = data.Token;
            return account;
        }
    }
}