using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;

namespace KursWebApplication.Business_Logic.Kurs3._2
{
    public class FirebaseLogic
    {
        FirebaseDataAccess dateAccess = new FirebaseDataAccess();
        public void addTokenLogic(int userId, string token)
        {
            dateAccess.addToken(userId, token);
        }

        public void refreshTokenLogic(int userId, string newToken)
        {
            dateAccess.refreshToken(userId, newToken);
        }
    }
}