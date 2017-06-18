using KursWebApplication.Data_Access;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class DriverLogic
    {
        DriverDataAccess dataAccess = new DriverDataAccess();

        public List<MyDBModels.Driver> logicMethodForGetListData()
        {
            List<MyDBModels.Driver> listData = dataAccess.getListData();
            return listData;
        }

        public MyDBModels.Driver logicMethodForGetData(int id)
        {
            MyDBModels.Driver data = dataAccess.getDataById(id);
            return data;
        }

        public void logicMethodForPostData(Models.DriverModel data)
        {
            dataAccess.postDriver(data);
        }

        public void logicMethodForDeleteDataint(int id)
        {
            dataAccess.deleteDriver(id);
        }
    }
}