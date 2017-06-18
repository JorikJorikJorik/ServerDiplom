using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Data_Access
{
    public class DriverDataAccess
    {
        public List<MyDBModels.Driver> getListData()
        {
            var db = new MyDBModels.DB();
            List<MyDBModels.Driver> listData = db.driver.ToList();
            return listData;
        }

        public MyDBModels.Driver getDataById(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Driver driverModel = db.driver.Where(b => b.DriverId == id).FirstOrDefault();
            return driverModel;
        }

        public void postDriver(Models.DriverModel value)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Driver driver = new MyDBModels.Driver();
            driver.Secondname = value.Secondname;
            driver.Qualification = value.Qualification;
            driver.Experience = value.Experience;
            driver.Salary = value.Salary;
            db.driver.Add(driver);
            db.SaveChanges();
        }


        public void deleteDriver(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Driver driver = db.driver.Where(d => d.DriverId == id).FirstOrDefault();
            if (driver != null)
            {
                db.driver.Remove(driver);
                db.SaveChanges();
            }
        }
    }
}