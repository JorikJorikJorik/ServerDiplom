using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Data_Access
{
    public class AccDataAccess
    {
        public int postDriver(Models.DriverAccountModel postDriver)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Account account = new MyDBModels.Account();
            MyDBModels.Driver driver = new MyDBModels.Driver();

            List<MyDBModels.Driver> listData = db.driver.ToList();
            List<int> num = new List<int>();
            int number;
            if (num.Count > 0)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    num.Add(listData[i].DriverNumber);
                }
                number = EncryptClass.GenerateUnikalNumber(num, 1, 100);
            }
            else
            {
                Random rund = new Random();
                number = rund.Next(1, 100);
            }

            account.LoginId = postDriver.AccountModel.Secondname;
            account.PasswordWorker = postDriver.AccountModel.Password;
            account.RoleWorker = postDriver.AccountModel.Role;
            account.NumberWorker = EncryptClass.DESEncrypt(number.ToString());
            account.Token = postDriver.AccountModel.Token;

            db.account.Add(account);

            postDriver.DriverModel.DriverNumber = number;

            DriverModel value = postDriver.DriverModel;
   
            driver.Secondname = value.Secondname;
            driver.Qualification = value.Qualification;
            driver.Experience = value.Experience;
            driver.DriverNumber = value.DriverNumber;
            driver.Salary = value.Salary;
            driver.Image = null;
            db.driver.Add(driver);
            db.SaveChanges();

            return number;
        }

        public int postDispatcher(Models.DispatcherAccountModel postDispatcher)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Account account = new MyDBModels.Account();
            MyDBModels.Dispatcher dispatcher = new MyDBModels.Dispatcher();

            List<MyDBModels.Dispatcher> listData = db.dispatcher.ToList();
            List<int> num = new List<int>();
            int number;
            if (num.Count > 0)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    num.Add(listData[i].DispatcherNumber);
                }
                number = EncryptClass.GenerateUnikalNumber(num, 101, 200);
            }
            else
            {
                Random rund = new Random();
                number = rund.Next(101, 200);
            }


            account.LoginId = postDispatcher.AccountModel.Secondname;
            account.PasswordWorker = postDispatcher.AccountModel.Password;
            account.RoleWorker = postDispatcher.AccountModel.Role;
            account.NumberWorker = EncryptClass.DESEncrypt(number.ToString());
            account.Token = postDispatcher.AccountModel.Token;

            db.account.Add(account);

            dispatcher.DispatcherNumber = number;
            dispatcher.Secondname = EncryptClass.DESDecrypt(postDispatcher.AccountModel.Secondname);
            dispatcher.Image = null;

            db.dispatcher.Add(dispatcher);

            db.SaveChanges();

            return number;

        }

        public List<string> postSignIn(Models.AccountModel accountModel)
        {
            var db = new MyDBModels.DB();

            string cryptName = EncryptClass.DESEncrypt(accountModel.Secondname);
            string cryptPassword = EncryptClass.MD5Hash(accountModel.Password);
            List<string> result = new List<string>();
            result.Clear();
            MyDBModels.Account accountFinal = db.account.Where(b => b.LoginId == cryptName && b.PasswordWorker == cryptPassword).FirstOrDefault();
            if (accountFinal != null)
            {
                result.Add(EncryptClass.DESDecrypt(accountFinal.LoginId));
                result.Add(EncryptClass.DESDecrypt(accountFinal.RoleWorker));
                result.Add(EncryptClass.DESDecrypt(accountFinal.NumberWorker));

                int number = int.Parse(EncryptClass.DESDecrypt(accountFinal.NumberWorker));
                if (number > 100) {
                 MyDBModels.Dispatcher dispatcher = db.dispatcher.Where(b => b.DispatcherNumber == number).FirstOrDefault();
                    result.Add(dispatcher.Image);
                }
                else {
                    MyDBModels.Driver driver = db.driver.Where(b => b.DriverNumber == number).FirstOrDefault();
                   result.Add(driver.Image);
                }
            }
            else result.Add("NOT OK");
            return result;

        }
    }
}
