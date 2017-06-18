using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class FirebaseSendMessageModel
    {
        public FirebaseDataModel data { get; set; }
        public string to { get; set; }
    }
}