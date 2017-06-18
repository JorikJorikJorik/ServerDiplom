using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;
using KursWebApplication.Models;

namespace KursWebApplication.Business_Logic
{
    public class ProfileLogic
    {
        ProfileDataAccess dataAccess = new ProfileDataAccess();

        public PreviewProfileModel previewData(int userId, int typePeriod)
        {
            return dataAccess.previewProfile(userId, typePeriod);
        }

        public int everyDayStatisticsLogic(int userId, EveryDayProfileStatisticsModel model)
        {
            return dataAccess.everyDayStatistics(userId, model);
        }

        internal PreviewProfileModel profileData(int userId)
        {
            return dataAccess.profileData(userId);
        }

        public ProfileStatisticsModel statisticByPeriodLogic(int userId, int typePeriod)
        {
            return dataAccess.statisticsTotalAndEveryDayBuPeriod(userId, typePeriod);
        }

        public List<DetailsWeekStatisticks> detilStatisticsdLogic(int userId, string startDate, int typePeriod)
        {
            return dataAccess.detailsWeekProfileStatistics(userId, startDate, typePeriod);
        }

        public TotalProfileModel totalData(int userId, int profileId)
        {
            return dataAccess.totalProfile(userId, profileId);
        }
    }
}