using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;
using KursWebApplication.Models;

namespace KursWebApplication.Business_Logic
{
    public class LogInLogic
    {
        LogInDataAccess access = new LogInDataAccess();

        public PreviewProfileModel logInUser(string basic)
        {
            return access.logInUserCheck(basic);
        }
    }
}