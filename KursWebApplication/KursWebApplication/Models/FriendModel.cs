using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class FriendModel
    {
        public int ProfileId { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public string City { get; set; }
        public DateTime stateLastActivity { get; set; }
    }
}