using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class ProfileShortModel
    {
        public int ProfieId { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastActive { get; set; }
        public bool Creater { get; set; }
    }
}