using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Models
{
    public class AccountModel
    {
        public string Secondname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Number { get; set; }
        public string Token { get; set; }
    }
}