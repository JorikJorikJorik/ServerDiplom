using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class GasListModel
    {
        public int GasListId { get; set; }
        public int BusId { get; set; }
        public int CountLitre { get; set; }
        public string TypeGas { get; set; }
        public int CostGas { get; set; }
        public DateTime TimeGet { get; set; }
    }
}