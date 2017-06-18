using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class DriverModel
    {
        public int DriverId { get; set; }
        public int DriverNumber { get; set; }
        public string Secondname { get; set; }
        public string Image { get; set; }
        public int Experience { get; set; }
        public int Salary { get; set; }
        public string Qualification { get; set; }
    }
}