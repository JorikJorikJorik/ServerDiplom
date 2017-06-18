using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class ProfileStatisticsModel
    {
        public double CountDistanceTotal { get; set; }
        public double MiddleSpeedTotal { get; set; }
        public int TimeInTripTotal { get; set; }
        public int CaloriesTotal { get; set; }
        public int CountDangerousSituation { get; set; }
        public int CountAttemptedTheft { get; set; }
        public List<EveryDayProfileStatisticsModel> EveryDayProfileStatistics { get; set; }
    }
}