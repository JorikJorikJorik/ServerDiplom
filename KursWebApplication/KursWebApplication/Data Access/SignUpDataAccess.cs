using KursWebApplication.Business_Logic;
using KursWebApplication.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace KursWebApplication.Data_Access
{
    public class SignUpDataAccess
    {
        public PreviewProfileModel signUpUserToDataBase(SignUpModel model, string baseData) {
            var db = new MyDBModels.DB();

            if (db.signUp.Where(s=> s.BaseCode == baseData).Count() > 0)
            {
                return null;
            }

            MyDBModels.EveryDayProfileStatistics everyDayProfileStatistics = new MyDBModels.EveryDayProfileStatistics();
            everyDayProfileStatistics.CountDistance = 0;
            everyDayProfileStatistics.MiddleSpeed = 0;
            everyDayProfileStatistics.TimeInTrip = TimeSpan.Zero;
            everyDayProfileStatistics.Calories = 0;
            everyDayProfileStatistics.TimeCreate = DateTime.Now;

            db.everyDayProfileStatistics.Add(everyDayProfileStatistics);
            db.SaveChanges();

            MyDBModels.ProfileStatistics profileStatistics = new MyDBModels.ProfileStatistics();
            profileStatistics.CountDistanceTotal = 0;
            profileStatistics.MiddleSpeedTotal = 0;
            profileStatistics.TimeInTripTotal = TimeSpan.Zero;
            profileStatistics.CaloriesTotal = 0;
            profileStatistics.CountDangerousSituation = 0;
            profileStatistics.CountAttemptedTheft = 0;
            profileStatistics.EveryDayProfileStatisticsIdArray = new int[] { db.everyDayProfileStatistics.OrderByDescending(i => i.EveryDayProfileStatisticsId).FirstOrDefault().EveryDayProfileStatisticsId };

            db.profileStatistics.Add(profileStatistics);
            db.SaveChanges();

            MyDBModels.Profile profile = new MyDBModels.Profile();
            profile.Name = model.Name;
            profile.LastName = model.LastName;
            profile.PhotoUrl = new PhotoUtils().generateRundomUrlPhoto();
            profile.Phone = model.Phone;
            profile.Email = model.Email;
            profile.City = model.City;
            profile.TimeLastActive = DateTime.Now;
            profile.ProfileStatisticsId = db.profileStatistics.OrderByDescending(i => i.ProfileStatisticsId).FirstOrDefault().ProfileStatisticsId;

            db.profile.Add(profile);
            db.SaveChanges();

            MyDBModels.User user = new MyDBModels.User();
            user.ProfileId = db.profile.OrderByDescending(i => i.ProfileId).FirstOrDefault().ProfileId;
            user.FriendIdArray = new int[] {};
            user.CommunicationIdArray = new int[] {};
            user.GroupIdArray = new int[] {};
            user.ArduinoIdArray = new int[] {};
            user.FirebaseToken = "";
            user.FriendPossibleIdArray = new int[] {};
            user.CommunicationPinIdArray = new int[] {};

            db.user.Add(user);
            db.SaveChanges();

            MyDBModels.SignUp signUp = new MyDBModels.SignUp();
            signUp.BaseCode = baseData;
            signUp.LoginEncode = model.Login;
            signUp.PasswordEncode = model.Password;
            signUp.UserId = db.user.OrderByDescending(i => i.UserId).FirstOrDefault().UserId;

            db.signUp.Add(signUp);
            db.SaveChanges();

            var dataAccess = new ProfileDataAccess();
            var userId = db.signUp.Where(s => s.BaseCode == baseData).First().UserId;
            var profileId = db.user.Where(u => u.UserId == userId).First().ProfileId;
            var profileDbModel = db.profile.Where(p => p.ProfileId == profileId).First();
            var profileModel = new ProfileModel();
            
            return dataAccess.previewProfile(userId, 1);
        }
    }
}