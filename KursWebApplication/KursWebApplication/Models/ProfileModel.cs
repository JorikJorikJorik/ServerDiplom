using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime TimeLastActive { get; set; }
        public ProfileStatisticsModel ProfileStatistics { get; set; }
    }
}