using KursWebApplication.Data_Access;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class SignUpLogic
    {
        SignUpDataAccess accesss = new SignUpDataAccess();

        public PreviewProfileModel signUpUser(SignUpModel model)
        {
            Crypto crypto = new Crypto();
            string baseData = crypto.encryptToBasicAuth(model.Login, model.Password);
            model.Login = crypto.encryptMD5(model.Login);
            model.Password = crypto.encryptMD5(model.Password);

           return accesss.signUpUserToDataBase(model, baseData);
        }
    }
}