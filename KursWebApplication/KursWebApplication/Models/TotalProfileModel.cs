using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class TotalProfileModel
    {
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime TimeLastActive { get; set; }
        public string KeyDialog { get; set; }
        public int CountFriends { get; set; }
        public int CountMutualFriends { get; set; }
        public int ProfileStatistics { get; set; }
        public int TypeUser { get; set; }
    }
}