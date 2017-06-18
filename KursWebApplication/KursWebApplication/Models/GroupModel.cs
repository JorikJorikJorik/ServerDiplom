using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class GroupModel
    {
        public string KeyGroup { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateFinish { get; set; }
        public int[] PositionParticipantProfileIdArray { get; set; }
    }
}