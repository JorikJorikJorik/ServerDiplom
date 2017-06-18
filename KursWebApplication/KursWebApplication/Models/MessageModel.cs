using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class MessageModel
    {
        public int ProfileId { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public string DataMessage { get; set; }
        public bool isRead { get; set; }
        public int TypeMessage { get; set; }
        public DateTime TimeWritten { get; set; }
    }
}