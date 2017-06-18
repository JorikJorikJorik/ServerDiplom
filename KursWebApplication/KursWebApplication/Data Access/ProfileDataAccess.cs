using KursWebApplication.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using KursWebApplication.Business_Logic;

namespace KursWebApplication.Data_Access
{
    public class ProfileDataAccess
    {
        public PreviewProfileModel previewProfile(int userId, int typePeriod)
        {

            /*var db = new MyDBModels.DB();

            PreviewProfileModel previewProfileModel = new PreviewProfileModel();

            var userModel = db.user.Where(u => u.UserId == userId).First();
            var profileModel = db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();

            var profileStatistics = db.profileStatistics.Where(ps => ps.ProfileStatisticsId == profileModel.ProfileStatisticsId).First();
         
            ProfileStatisticsModel profileStatisticsModel = new ProfileStatisticsModel();

            profileStatisticsModel.CountDistanceTotal = profileStatistics.CountDistanceTotal;
            profileStatisticsModel.MiddleSpeedTotal = profileStatistics.MiddleSpeedTotal;
            profileStatisticsModel.TimeInTripTotal = (int) profileStatistics.TimeInTripTotal.TotalSeconds;
            profileStatisticsModel.CaloriesTotal = profileStatistics.CaloriesTotal;
            profileStatisticsModel.CountDangerousSituation = profileStatistics.CountDangerousSituation;
            profileStatisticsModel.CountAttemptedTheft = profileStatistics.CountAttemptedTheft;
            profileStatisticsModel.EveryDayProfileStatistics = statisticsByPeriod(userId, typePeriod);

            */
            ProfileStatisticsModel profileStatisticsModel = formatTotalStatisitcs(userId, statisticsByPeriod(userId, typePeriod));
            return formatProfileData(userId, profileStatisticsModel);
        }

        public PreviewProfileModel profileData(int userId)
        {
           return formatProfileData(userId, null);
        }

        public int everyDayStatistics(int userId, EveryDayProfileStatisticsModel model) {
            int statisticsId = -1;
            if (model.EveryDayProfileStatisticsId > 0)
            {
                statisticsId = everyDayStatisticsUpdate(userId, model);
                updateTotalStatistics(userId, model);
            }
            else {
                statisticsId = everyDayStatisticsCreate(userId, model);
            }
            return statisticsId;
        }

        public ProfileStatisticsModel statisticsTotalAndEveryDayBuPeriod(int userId, int typePeriod)
        {
            return formatTotalStatisitcs(userId, statisticsByPeriod(userId, typePeriod));
        }


        private List<EveryDayProfileStatisticsModel> statisticsByPeriod(int userId, int typePeriod)
        {

            List<MyDBModels.EveryDayProfileStatistics> allMyEverydayStatisticsList = createAllEveryDayStatisticsList(userId);

            switch (typePeriod)
            {
                case 1: return searchByYear(allMyEverydayStatisticsList);
                case 2: return searchByMonth(allMyEverydayStatisticsList);
                case 3: return searchByWeek(allMyEverydayStatisticsList);
                default:
                    return new List<EveryDayProfileStatisticsModel>();
            }       
        }
        
        public List<DetailsWeekStatisticks> detailsWeekProfileStatistics(int userId, string dateStartString, int typePeriod)
        {
            DateUtils dateUtils1 = new DateUtils();
            DateTime dateStart = dateUtils1.convertStringToDateWithoutTime(dateStartString);

            List<MyDBModels.EveryDayProfileStatistics> allMyEverydayStatisticsList = createAllEveryDayStatisticsList(userId);
            DateUtils dateUtils = new DateUtils(dateStart.Year);
            List<DetailsWeekStatisticks> seachStatisticsWeekByPeriod = new List<DetailsWeekStatisticks>();

            DateTime dateFinish;

            bool conditionLimitDate = dateStart.Year < DateTime.Now.Year;
            switch (typePeriod)
            {
                case 1: dateFinish = new DateTime(dateStart.Year, conditionLimitDate ? 12 : DateTime.Now.Month, conditionLimitDate ? 31 : DateTime.Now.Day);
                    break;
                case 2: dateFinish = dateUtils.calculateRightNextMonth(dateStart);
                    break;
                case 3: dateFinish = dateUtils.calculateRightNextWeek(dateStart);
                    break;
                default: dateFinish = DateTime.Now;
                    break;
            }

            var weekStart = dateUtils.getWeekNumberByDateTime(dateStart);
            var weekFinish = dateUtils.getWeekNumberByDateTime(dateFinish);
            var periodSearch = Enumerable.Range(weekStart, weekFinish - weekStart + 1).ToArray();

              foreach (int itemPeriod in periodSearch)
              {
                  var periodStatistics = allMyEverydayStatisticsList.Where(s => s.TimeCreate.Year == dateStart.Year && dateUtils.getWeekNumberByDateTime(s.TimeCreate) == itemPeriod);
                  string fullMonthName = dateUtils.convertDateToWeekIntervalLable(dateUtils.firstDateOfWeekISO8601(itemPeriod - 1));
                  List<MyDBModels.EveryDayProfileStatistics> periodStatisticsWeekList = fillEmptyDayInWeek(periodStatistics, dateUtils.getPeriodDayMonthByWeekNumber(itemPeriod - 1));
                  seachStatisticsWeekByPeriod.Add(createModelDetailsWeek(periodStatisticsWeekList, fullMonthName));
              }

            return seachStatisticsWeekByPeriod;
        }

        public TotalProfileModel totalProfile(int userId, int profileId)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
            TotalProfileModel model = new TotalProfileModel();

            var myUserModel = db.user.Where(u => u.UserId == userId).First();
            var myProfileModel = db.profile.Where(p => p.ProfileId == myUserModel.ProfileId).First();
            int typeUser = 0;
            int countMutatualFriends = 0;

            if (myProfileModel.ProfileId == profileId)
            {
                typeUser = 1;
            }
            else
            {
                var userIdProfile = db.user.Where(u => u.ProfileId == profileId).First();
                var friendArrayUser = userIdProfile.FriendIdArray;
                if (friendArrayUser.Count() > 0)
                {
                    /* foreach (int firendId in friendArrayUser)
                     {
                       var friendMeForHim = db.profile.Where(p => p.ProfileId == myProfileModel.ProfileId);

                         if (myUserModel.FriendIdArray.Count() > 0) {
                             foreach (int myFriendId in myUserModel.FriendIdArray)
                             {
                                 var myFirendProfileId = db.friend.Where(f => f.FriendId == myFriendId).First().ProfileId;
                                 var mutaulFriend = db.friend.Where(f => f.FriendId == firendId && f.ProfileId == myFirendProfileId);
                                 if (mutaulFriend.Count() > 0) { countMutatualFriends++; }
                             }
                         }
                                    typeUser = friendMeForHim.Count() > 0 ? 2 : 3;
             
                     }*/

                    var friendMeForHim = friendArrayUser.Where(f => f == myProfileModel.ProfileId);
                    if (myUserModel.FriendIdArray.Count() > 0)
                    {
                        foreach (int myFriendId in myUserModel.FriendIdArray)
                        {
                            var mutaulFriend = friendArrayUser.Where(f => f == myFriendId);
                            if (mutaulFriend.Count() > 0) { countMutatualFriends++; }
                        }
                    }
                    typeUser = friendMeForHim.Count() > 0 ? 2 : 3;
                }
                else
                {
                    typeUser = 3;
                }
            }

            var searchProfile = db.profile.Where(p => p.ProfileId == profileId).First();
            var searchProfileFiends = db.user.Where(u => u.ProfileId == searchProfile.ProfileId).First().FriendIdArray;
            

            model.FullName = String.Format("{0} {1}", searchProfile.Name, searchProfile.LastName);
            model.PhotoUrl = searchProfile.PhotoUrl;
            model.City = searchProfile.City;
            model.Phone = searchProfile.Phone;
            model.Email = searchProfile.Email;
            model.TimeLastActive = searchProfile.TimeLastActive;// dateUtils.calculateStateLastActivity(searchProfile.TimeLastActive);
            model.KeyDialog = typeUser == 2 ? searchKeyDailog(myUserModel.UserId, profileId) : "";
            model.CountFriends = searchProfileFiends.Count();
            model.CountMutualFriends = typeUser > 1 ? countMutatualFriends : 0;
            model.ProfileStatistics = typeUser > 2 ? 0 : searchProfile.ProfileStatisticsId;
            model.TypeUser = typeUser;

            return model;
        }

        public string searchKeyDailog(int myUserId, int friendProfileId)
        {
            var db = new MyDBModels.DB();
            var myUserModel = db.user.Where(u => u.UserId == myUserId).First();
            var myFriendProfileModel = db.profile.Where(u => u.ProfileId == friendProfileId).First();

            foreach (MyDBModels.Communication communication in db.communication)
            {
                if (!communication.IsGroup && communication.ParticipantProfileIdArray.Count() == 2 && communication.ParticipantProfileIdArray.Contains(myUserModel.ProfileId) && communication.ParticipantProfileIdArray.Contains(friendProfileId))
                    return communication.KeyDialog;
            };
            return "";
        }

        private ProfileStatisticsModel formatTotalStatisitcs(int userId, List<EveryDayProfileStatisticsModel> everyDayList)
        {
            var db = new MyDBModels.DB();

        
            var userModel = db.user.Where(u => u.UserId == userId).First();
            var profileModel = db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();

            var profileStatistics = db.profileStatistics.Where(ps => ps.ProfileStatisticsId == profileModel.ProfileStatisticsId).First();

            ProfileStatisticsModel profileStatisticsModel = new ProfileStatisticsModel();

            profileStatisticsModel.CountDistanceTotal = profileStatistics.CountDistanceTotal;
            profileStatisticsModel.MiddleSpeedTotal = profileStatistics.MiddleSpeedTotal;
            profileStatisticsModel.TimeInTripTotal = (int)profileStatistics.TimeInTripTotal.TotalSeconds;
            profileStatisticsModel.CaloriesTotal = profileStatistics.CaloriesTotal;
            profileStatisticsModel.CountDangerousSituation = profileStatistics.CountDangerousSituation;
            profileStatisticsModel.CountAttemptedTheft = profileStatistics.CountAttemptedTheft;
            profileStatisticsModel.EveryDayProfileStatistics = new List<EveryDayProfileStatisticsModel>(everyDayList);

            return profileStatisticsModel;
        }

        private List<MyDBModels.EveryDayProfileStatistics> fillEmptyDayInWeek(IEnumerable<MyDBModels.EveryDayProfileStatistics> periodStatistics, int [] numberPeriod)
        {
            List<MyDBModels.EveryDayProfileStatistics> result = new List<MyDBModels.EveryDayProfileStatistics>();
            foreach (int number in numberPeriod)
            {
               var model = periodStatistics.Where(p => p.TimeCreate.Day == number);
               result.Add(model.Count() > 0 ? model.First() : createEmptyDataDBEveryDayStatistics());
            }
            return result;
        }

        private DetailsWeekStatisticks createModelDetailsWeek1(int data1, int data2)
        {
            DetailsWeekStatisticks detailsWeek = new DetailsWeekStatisticks();

            detailsWeek.Longest = data1.ToString();
            detailsWeek.Shortest = data2.ToString();
            detailsWeek.MiddleEffective = 0.ToString();
            detailsWeek.CountEffectiveDay = 0.ToString();
            detailsWeek.Distance = new List<double>();
            detailsWeek.Speed = new List<double>();
            detailsWeek.TimeInTrip = new List<long>();
            detailsWeek.Calories = new List<int>();

            return detailsWeek;
        }

        private MyDBModels.EveryDayProfileStatistics createEmptyDataDBEveryDayStatistics()
        {
            MyDBModels.EveryDayProfileStatistics statistics = new MyDBModels.EveryDayProfileStatistics();
            statistics.CountDistance = 0;
            statistics.MiddleSpeed = 0;
            statistics.TimeInTrip = new TimeSpan();
            statistics.Calories = 0;
            statistics.TimeCreate = new DateTime();

            return statistics;
        }

        private DetailsWeekStatisticks createModelDetailsWeek(List<MyDBModels.EveryDayProfileStatistics> periodStatistics, string nameDate)
        {
            DetailsWeekStatisticks detailsWeek = new DetailsWeekStatisticks();

            detailsWeek.PeriodName = nameDate;
            detailsWeek.Longest = (periodStatistics.Count() > 0 ? periodStatistics.Max(p => p.CountDistance) : 0).ToString();
            detailsWeek.Shortest = (periodStatistics.Count() > 0 ? periodStatistics.Min(p => p.CountDistance) : 0).ToString();
            detailsWeek.MiddleEffective = (periodStatistics.Count() > 0 ? ((int)(periodStatistics.Sum(p => p.CountDistance) / 7)) : 0).ToString();
            detailsWeek.CountEffectiveDay = (periodStatistics.Count() > 0 ? periodStatistics.Count(p => p.CountDistance > 0) : 0).ToString();
            detailsWeek.Distance = getCountDistanceListFromObject(periodStatistics);
            detailsWeek.Speed = geMiddleSpeedtListFromObject(periodStatistics);
            detailsWeek.TimeInTrip = getIntervalListFromObject(periodStatistics);
            detailsWeek.Calories = getCaloriesListFromObject(periodStatistics);
         
            return detailsWeek;
        }

        private List<double> getCountDistanceListFromObject(List<MyDBModels.EveryDayProfileStatistics> periodStatistics)
        {
            List<double> list = new List<double>();
            foreach (MyDBModels.EveryDayProfileStatistics item in periodStatistics)
            {
                list.Add(periodStatistics.Count() > 0 ? item.CountDistance : 0);
            }
            return list;
        }

        private List<double> geMiddleSpeedtListFromObject(List<MyDBModels.EveryDayProfileStatistics> periodStatistics)
        {
            List<double> list = new List<double>();
            foreach (MyDBModels.EveryDayProfileStatistics item in periodStatistics)
            {
                list.Add(periodStatistics.Count() > 0 ? item.MiddleSpeed : 0);
            }
            return list;
        }

        private List<long> getIntervalListFromObject(List<MyDBModels.EveryDayProfileStatistics> periodStatistics)
        {
            List<long> list = new List<long>();
            foreach (MyDBModels.EveryDayProfileStatistics item in periodStatistics)
            {
                list.Add((periodStatistics.Count() > 0 ? (long)(item.TimeInTrip.TotalSeconds*10000000) : (long)(new TimeSpan().TotalSeconds*10000000)));
            }
            return list;
        }

        private List<int> getCaloriesListFromObject(List<MyDBModels.EveryDayProfileStatistics> periodStatistics)
        {
            List<int> list = new List<int>();
            foreach (MyDBModels.EveryDayProfileStatistics item in periodStatistics)
            {
                list.Add(periodStatistics.Count() > 0 ? item.Calories : 0);
            }
            return list;
        }

        private List<MyDBModels.EveryDayProfileStatistics> createAllEveryDayStatisticsList(int userId)
        {
            var db = new MyDBModels.DB();
            var profileStatisticsModel = getStatisticsByUserId(db, userId);

            var everyDayProfileStatisticsIdArray = profileStatisticsModel.EveryDayProfileStatisticsIdArray;
            var allMyEverydayStatisticsList = new List<MyDBModels.EveryDayProfileStatistics>();

            foreach (int itemId in everyDayProfileStatisticsIdArray)
            {
                allMyEverydayStatisticsList.Add(db.everyDayProfileStatistics.Where(e => e.EveryDayProfileStatisticsId == itemId).First());
            }

            return allMyEverydayStatisticsList;
        }

        private int everyDayStatisticsCreate(int userId, EveryDayProfileStatisticsModel model)
        {
            var db = new MyDBModels.DB();
            var dateUtils = new DateUtils();
            var profileStatisticsModel = getStatisticsByUserId(db, userId);

            DateTime inputDate = model.TimeCreate;
           
                foreach (int everyDayId in profileStatisticsModel.EveryDayProfileStatisticsIdArray) {
                    var modelEveryDay = db.everyDayProfileStatistics.Where(e => e.EveryDayProfileStatisticsId == everyDayId).First();
                    if(modelEveryDay.TimeCreate.Year == inputDate.Year && modelEveryDay.TimeCreate.Month == inputDate.Month && modelEveryDay.TimeCreate.Day == inputDate.Day) return modelEveryDay.EveryDayProfileStatisticsId;
                }
            
                MyDBModels.EveryDayProfileStatistics everyDayStatistics = new MyDBModels.EveryDayProfileStatistics();
                everyDayStatistics.CountDistance = 0;
                everyDayStatistics.MiddleSpeed = 0;
                everyDayStatistics.TimeInTrip = new TimeSpan(0 * 10000000);
                everyDayStatistics.Calories = 0;
                everyDayStatistics.TimeCreate = DateTime.Now;

                db.everyDayProfileStatistics.Add(everyDayStatistics);
                db.SaveChanges();

                var lastStatisticsId = db.everyDayProfileStatistics.OrderByDescending(i => i.EveryDayProfileStatisticsId).FirstOrDefault().EveryDayProfileStatisticsId;

                var everyDayArrayStatisticsIdArray = profileStatisticsModel.EveryDayProfileStatisticsIdArray;
                int count = everyDayArrayStatisticsIdArray.Count();
                var everyDayArrayStatisticsIdArrayUpdate = new int[count + 1];
                Array.Copy(everyDayArrayStatisticsIdArray, everyDayArrayStatisticsIdArrayUpdate, count);
                everyDayArrayStatisticsIdArrayUpdate[count] = lastStatisticsId;

                profileStatisticsModel.EveryDayProfileStatisticsIdArray = everyDayArrayStatisticsIdArrayUpdate;

                /*правильнее
                var everyDayArrayStatisticsIdArray = profileStatisticsModel.EveryDayProfileStatisticsIdArray.ToList();
                everyDayArrayStatisticsIdArray.Add(db.everyDayProfileStatistics.OrderByDescending(i => i.EveryDayProfileStatisticsId).FirstOrDefault().EveryDayProfileStatisticsId);
                profileStatisticsModel.EveryDayProfileStatisticsIdArra = everyDayArrayStatisticsIdArray.ToArray();
                */

                db.SaveChanges();
            
            return lastStatisticsId;
        }

        private int everyDayStatisticsUpdate(int userId, EveryDayProfileStatisticsModel model)
        {
            var db = new MyDBModels.DB();
            var profileModel = getProfileDbByUserId(db, userId);
            var everyDayProfileStatisticsUpdate = db.everyDayProfileStatistics.Where(ps => ps.EveryDayProfileStatisticsId == model.EveryDayProfileStatisticsId).First();

            everyDayProfileStatisticsUpdate.CountDistance = model.CountDistance;
            everyDayProfileStatisticsUpdate.MiddleSpeed = model.MiddleSpeed;
            everyDayProfileStatisticsUpdate.TimeInTrip = new TimeSpan(model.TimeInTrip) ;
            everyDayProfileStatisticsUpdate.Calories = model.Calories;

            db.SaveChanges();

            return everyDayProfileStatisticsUpdate.EveryDayProfileStatisticsId;
        }

        private void updateTotalStatistics(int userId, EveryDayProfileStatisticsModel mode)
        {
            var db = new MyDBModels.DB();
            var profileStatisticsModel = getStatisticsByUserId(db, userId);
            var everyDayProfileStatisticsIdArray = profileStatisticsModel.EveryDayProfileStatisticsIdArray;
            var everyDayProfileStatisticsMyList = new List<MyDBModels.EveryDayProfileStatistics>();

            foreach (int itemId in everyDayProfileStatisticsIdArray)
            {
                everyDayProfileStatisticsMyList.Add(db.everyDayProfileStatistics.Where(e => e.EveryDayProfileStatisticsId == itemId).First());
            }

            profileStatisticsModel.CountDistanceTotal = everyDayProfileStatisticsMyList.Average(e => e.CountDistance);
            profileStatisticsModel.MiddleSpeedTotal = everyDayProfileStatisticsMyList.Average(e => e.MiddleSpeed); ;
            profileStatisticsModel.TimeInTripTotal = new TimeSpan((long)everyDayProfileStatisticsMyList.Average(e => e.TimeInTrip.Ticks));
            profileStatisticsModel.CaloriesTotal = (int)everyDayProfileStatisticsMyList.Average(e => e.Calories);

            db.SaveChanges();
        }

        private List<EveryDayProfileStatisticsModel> searchByYear(List<MyDBModels.EveryDayProfileStatistics> allMyEverydayStatisticsList)
        {
            var seachStatisticsByPeriod = new List<EveryDayProfileStatisticsModel>();

            int firstPeriod = allMyEverydayStatisticsList.First().TimeCreate.Year;
            var periodSearch = Enumerable.Range(firstPeriod, DateTime.Now.Year - firstPeriod + 1).ToArray();

            foreach (int itemPeriod in periodSearch)
            {
                var periodStatistics = allMyEverydayStatisticsList.Where(s => s.TimeCreate.Year == itemPeriod);
                seachStatisticsByPeriod.Add(createModelEveryday(periodStatistics, itemPeriod.ToString(), 12));
     
            }
            seachStatisticsByPeriod.Reverse();
            return seachStatisticsByPeriod;
        }

        private List<EveryDayProfileStatisticsModel> searchByMonth(List<MyDBModels.EveryDayProfileStatistics> allMyEverydayStatisticsList)
        {
            var seachStatisticsByPeriod = new List<EveryDayProfileStatisticsModel>();

            int firstPeriod = allMyEverydayStatisticsList.Where(s => s.TimeCreate.Year == DateTime.Now.Year).First().TimeCreate.Month;
            var periodSearch = Enumerable.Range(firstPeriod, DateTime.Now.Month - firstPeriod + 1).ToArray();

            foreach (int itemPeriod in periodSearch)
            {
                var periodStatistics = allMyEverydayStatisticsList.Where(s => s.TimeCreate.Year == DateTime.Now.Year && s.TimeCreate.Month == itemPeriod);
                string fullMonthName = new DateTime(2015, itemPeriod, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
                seachStatisticsByPeriod.Add(createModelEveryday(periodStatistics, fullMonthName, DateTime.DaysInMonth(DateTime.Now.Year, itemPeriod)));
            }

            seachStatisticsByPeriod.Reverse();
            return seachStatisticsByPeriod;
        }

        private List<EveryDayProfileStatisticsModel> searchByWeek(List<MyDBModels.EveryDayProfileStatistics> allMyEverydayStatisticsList)
        {
            var dateUtils = new DateUtils(DateTime.Now.Year);
            var seachStatisticsByPeriod = new List<EveryDayProfileStatisticsModel>();

            int firstPeriod = dateUtils.getWeekNumberByDateTime(allMyEverydayStatisticsList.Where(s => s.TimeCreate.Year == DateTime.Now.Year).First().TimeCreate);
            var periodSearch = Enumerable.Range(firstPeriod, dateUtils.getWeekNumberByDateTime(DateTime.Now) - firstPeriod + 1).ToArray();

            foreach (int itemPeriod in periodSearch)
            {
                var periodStatistics = allMyEverydayStatisticsList.Where(s => s.TimeCreate.Year == DateTime.Now.Year && dateUtils.getWeekNumberByDateTime(s.TimeCreate) == itemPeriod);
                string fullMonthName = dateUtils.convertDateToWeekIntervalLable(dateUtils.firstDateOfWeekISO8601(itemPeriod-1));
                seachStatisticsByPeriod.Add(createModelEveryday(periodStatistics, fullMonthName, 7));
            }

            seachStatisticsByPeriod.Reverse();
            return seachStatisticsByPeriod;
        }

        private EveryDayProfileStatisticsModel createModelEveryday(IEnumerable<MyDBModels.EveryDayProfileStatistics> periodStatistics, string nameDate, int count)
        {
            var statisticsModel = new EveryDayProfileStatisticsModel();
            statisticsModel.CountDistance = (periodStatistics.Count() > 0 ? periodStatistics.Sum(e => e.CountDistance) / count : 0);
            statisticsModel.MiddleSpeed = (periodStatistics.Count() > 0 ? periodStatistics.Sum(e => e.MiddleSpeed) / count : 0);
            statisticsModel.TimeInTrip = (int) (periodStatistics.Count() > 0 ? new TimeSpan((long)periodStatistics.Sum(e => e.TimeInTrip.Ticks) / count) : new TimeSpan()).TotalSeconds;
            statisticsModel.Calories = (periodStatistics.Count() > 0 ? (int)periodStatistics.Sum(e => e.Calories) / count : 0);
            statisticsModel.NameDate = nameDate;
            return statisticsModel;
        }

        private MyDBModels.Profile getProfileDbByUserId(MyDBModels.DB db, int userId)
        {
            var userModel = db.user.Where(u => u.UserId == userId).First();
            return db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();
        }

        private MyDBModels.ProfileStatistics getStatisticsByUserId(MyDBModels.DB db, int userId)
        {
            var userModel = db.user.Where(u => u.UserId == userId).First();
            var profileModel = db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();
            return db.profileStatistics.Where(ps => ps.ProfileStatisticsId == profileModel.ProfileStatisticsId).First();

        }

        private bool IsUnreadCommunication(int[] messageIdArray, int myProfileId)
        {
            var db = new MyDBModels.DB();
            if (messageIdArray.Count() > 0)
            {
                foreach(int messageId in messageIdArray)
                {
                    var messageModel = db.message.Where(m => m.MessageId == messageId).First();
                    if (!messageModel.IsReadProfileId.Contains(myProfileId)) { return true; }
                };
            }
            return false;
        }

        private PreviewProfileModel formatProfileData(int userId, ProfileStatisticsModel profileStatisticsModel) {

            var db = new MyDBModels.DB();
            var countUnreadMessages = 0;
            var countGroups = 0;
            var dateUtils = new DateUtils();

            var userModel = db.user.Where(u => u.UserId == userId).First();
            var profileModel = db.profile.Where(p => p.ProfileId == userModel.ProfileId).First();

            var communicationIdArray = userModel.CommunicationIdArray;
            var communicationDataAccess = new CommunicationDataAccess();
            communicationIdArray.ToList().ForEach(delegate (int communicationId)
            {
                var communicationModelDb = db.communication.Where(c => c.CommunicationId == communicationId).First();
                if (IsUnreadCommunication(communicationModelDb.MessageIdArray, profileModel.ProfileId))
                    countUnreadMessages++;
            });

            PreviewProfileModel previewProfileModel = new PreviewProfileModel();

            previewProfileModel.FullName = String.Format("{0} {1}", profileModel.Name, profileModel.LastName);
            previewProfileModel.PhotoUrl = profileModel.PhotoUrl;
            previewProfileModel.TimeLastActive = profileModel.TimeLastActive;//dateUtils.calculateStateLastActivity(profileModel.TimeLastActive);
            previewProfileModel.CountRequestedFriends = userModel.FriendPossibleIdArray.Count(); ;
            previewProfileModel.CountUnreadMessages = countUnreadMessages;
            previewProfileModel.CountRequestedGroups = countGroups;
            previewProfileModel.ProfileStatistics = profileStatisticsModel;

            return previewProfileModel;
        }

    }
}