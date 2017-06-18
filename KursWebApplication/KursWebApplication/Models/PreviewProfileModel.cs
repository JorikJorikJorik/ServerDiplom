using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class PreviewProfileModel
    {
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime TimeLastActive { get; set; }
        public int CountRequestedFriends { get; set; }
        public int CountUnreadMessages { get; set; }
        public int CountRequestedGroups { get; set; }
        public ProfileStatisticsModel ProfileStatistics { get; set; }
    }
}