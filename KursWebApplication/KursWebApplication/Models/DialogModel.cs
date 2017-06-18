using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class DialogModel
    {
        public DialogInfoModel DialogInfo { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}