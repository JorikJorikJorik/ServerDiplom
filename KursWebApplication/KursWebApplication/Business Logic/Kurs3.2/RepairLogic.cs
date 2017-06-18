using KursWebApplication.Data_Access;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class RepairLogic
    {
        RepairDataAccess dataAccess = new RepairDataAccess();

    
        public List<FullRepairList> logicMethodForGetFullListData()
        {
            List<FullRepairList> listData = dataAccess.getFullListData();
            return listData;
        }

        public List<FullRepairList> logicMethodForGetFullData(int number)
        {
            List<FullRepairList> data = dataAccess.getFullListDataByUser(number);
            return data;
        }

        public List<MyDBModels.RepairList> logicMethodForGetListData()
        {
            List<MyDBModels.RepairList> listData = dataAccess.getListData();
            return listData;
        }

        public MyDBModels.RepairList logicMethodForGetData(int id)
        {
            MyDBModels.RepairList data = dataAccess.getDataById(id);
            return data;
        }

        public void logicMethodForPostData(Models.RepairListModel data, int number)
        {
            dataAccess.postRepair(data, number);
        }

        public void logicMethodForDeleteDataint(int id)
        {
            dataAccess.deleteRepair(id);
        }
    }
}