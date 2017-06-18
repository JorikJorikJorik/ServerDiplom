using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class Crypto
    {
        public string encryptToBasicAuth(string login, string password)
        {
            var textBase = String.Format("{0}:{1}", login, password);
            var textBaseInByte = System.Text.Encoding.UTF8.GetBytes(textBase);
            var base64 = Convert.ToBase64String(textBaseInByte);
            return String.Format("{0} {1}", "Basic", base64);
        }

        public string encryptMD5(string dataInput)
        {
            MD5 md5Hash = MD5.Create();
            StringBuilder sBuilder = new StringBuilder();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(dataInput));
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}