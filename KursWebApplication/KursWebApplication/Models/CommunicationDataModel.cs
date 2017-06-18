using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class CommunicationDataModel
    {
        public int CommunicationId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsGroup { get; set; }
        public string LastWrittenName { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastDate { get; set; }
        public bool IsRead { get; set; }
        public int CountUnreadMessage { get; set; }
       
    }
}