using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class EveryDayProfileStatisticsModel
    {
        public int EveryDayProfileStatisticsId { get; set; }
        public double CountDistance { get; set; }
        public double MiddleSpeed { get; set; }
        public long TimeInTrip { get; set; }
        public int Calories { get; set; }
        public DateTime TimeCreate { get; set; }
        public string NameDate { get; set; }
    }
}