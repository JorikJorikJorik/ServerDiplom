using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class MessageDeleteListModel
    {
        public string KeyDialog { get; set; }
        public int[] MessageIdArray { get; set; }
    }
}