using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class FullWorkList
    {
        public WorkListModel WorkList { get; set; }
        public BusModel Bus { get; set; }
        public DriverModel Driver { get; set; }
    }
}