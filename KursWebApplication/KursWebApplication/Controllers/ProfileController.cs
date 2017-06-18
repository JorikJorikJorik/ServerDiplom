using KursWebApplication.Business_Logic;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using KursWebApplication.Business_Logic.Kurs3._2;
using System.Net;

namespace KursWebApplication.Controllers
{
    public class ProfileController : ApiController
    {
        ProfileLogic logic = new ProfileLogic();

        [Route("api/Profile/ProfileData")]
        [HttpGet]
        public PreviewProfileModel getProfileData()
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.profileData(authorization.getUserId());
        }

        [Route("api/Profile/PreviewProfileData")]
        [HttpGet]
        public PreviewProfileModel getPreviewProfileData(int typePeriod)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.previewData(authorization.getUserId(), typePeriod);
        }

        [Route("api/Profile/EveryDayStatistics")]
        [AcceptVerbs("POST","PUT")]
        public int everyDayStatistics(EveryDayProfileStatisticsModel model)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.everyDayStatisticsLogic(authorization.getUserId(), model);
        }

        [Route("api/Profile/Statistics")]
        [HttpGet]
        public ProfileStatisticsModel everyDayStatistics(int typePeriod)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.statisticByPeriodLogic(authorization.getUserId(), typePeriod);
        }

        [Route("api/Profile/DetailsStatistics")]
        [HttpGet]
        public List<DetailsWeekStatisticks> detailsStatistics(string dateStart, int typePeriod)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.detilStatisticsdLogic(authorization.getUserId(), dateStart, typePeriod);
        }

        [Route("api/Profile/TotalProfileData")]
        [HttpGet]
        public TotalProfileModel detailsStatistics(int profileId)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.totalData(authorization.getUserId(), profileId);
        }
    }
}