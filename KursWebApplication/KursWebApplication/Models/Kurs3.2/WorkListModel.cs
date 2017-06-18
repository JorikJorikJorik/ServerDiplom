using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class WorkListModel
    {
        public int WorkListId { get; set; }
        public int DriverId { get; set; }
        public int BusId { get; set; }
        public string SecondNameDispatcher { get; set; }
        public DateTime DateAction { get; set; }
    }
}