using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Models;

namespace KursWebApplication.Models
{
    public class FullFriendModel
    {
        public int TypeUser { get; set; }
        public List<FriendModel> AllFirend { get; set; }
        public List<FriendModel> OnlineFirend { get; set; }
        public List<FriendModel> ThirdColumnFirend { get; set; }

    }
}