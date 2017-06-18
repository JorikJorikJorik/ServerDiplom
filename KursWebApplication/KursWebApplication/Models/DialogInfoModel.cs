using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class DialogInfoModel
    {
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime SecondInfoTime { get; set; }
        public string SecondInfoCount { get; set; }
        public int ProfileId { get; set; }
        public List<ProfileShortModel> ProfileShortModelList { get; set; }
    }
}