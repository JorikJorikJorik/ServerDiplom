using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class CommunicationModel
    {
        public string KeyDialog { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int[] ParticipantProfileIdArray { get; set; }
        public int[] MessageIdArray { get; set; }
    }
}