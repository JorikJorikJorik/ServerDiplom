using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class RepairListModel
    {
        public int ServiceListId { get; set; }
        public int BusId { get; set; }
        public string BusCondition { get; set; }
        public DateTime TimeGet { get; set; }
    }

}