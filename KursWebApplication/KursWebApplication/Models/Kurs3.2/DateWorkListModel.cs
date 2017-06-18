using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class DateWorkListModel
    {
        public int DateId { get; set; }
        public DateTime DateAction { get; set; }
        public int WorkListId { get; set; }
       
    }
}