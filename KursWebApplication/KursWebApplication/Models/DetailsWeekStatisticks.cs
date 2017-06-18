using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class DetailsWeekStatisticks
    {
        public string PeriodName { get; set; }
        public string Longest { get; set; }
        public string Shortest { get; set; }
        public string MiddleEffective { get; set; }
        public string CountEffectiveDay { get; set; }
        public List<double> Distance{get; set;}
        public List<double> Speed{get; set;}
        public List<long> TimeInTrip{get; set;}
        public List<int> Calories{get; set;}
    }
}