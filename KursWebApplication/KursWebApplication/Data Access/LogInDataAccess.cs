using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Business_Logic;
using KursWebApplication.Models;

namespace KursWebApplication.Data_Access
{
    public class LogInDataAccess
    {
        public PreviewProfileModel logInUserCheck(string basic) {
            var db = new MyDBModels.DB();

            try
            {
                var dataAccess = new ProfileDataAccess();
                var userId = db.signUp.Where(s => s.BaseCode == basic).First().UserId;
                var profileId = db.user.Where(u => u.UserId == userId).First().ProfileId;
                var profileDbModel = db.profile.Where(p => p.ProfileId == profileId).First();
                var profileModel = new ProfileModel();
                var dateUtils = new DateUtils();

                return dataAccess.previewProfile(userId, 1);

                /*
                profileModel.Name = profileDbModel.Name;
                profileModel.LastName = profileDbModel.LastName;
                profileModel.City = profileDbModel.City;
                profileModel.PhotoUrl = profileDbModel.PhotoUrl;
                profileModel.Phone = profileDbModel.Phone;
                profileModel.Email = profileDbModel.Email;
                profileModel.TimeLastActive = profileDbModel.TimeLastActive;// dateUtils.calculateStateLastActivity(profileDbModel.TimeLastActive);
               
                return profileModel;*/
            }
            catch (Exception exc)
            {
                return null;
            }
        }
    }
}