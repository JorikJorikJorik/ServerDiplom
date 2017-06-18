using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class UserModel
    {
        public ProfileModel Profile { get; set; }
        public ProfileModel[] Friends { get; set; }
        public CommunicationModel[] Communications { get; set; }
        public GroupModel[] Groups { get; set; }
        public ArduinoModel[] Arduino { get; set; }
        public string FirebaseToken { get; set; }
        public CommunicationModel[] CommunicationsPin { get; set; }
    }
}