using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class CommunicationShortModel
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int[] ParticipantProfileIdArray { get; set; }
    }
}