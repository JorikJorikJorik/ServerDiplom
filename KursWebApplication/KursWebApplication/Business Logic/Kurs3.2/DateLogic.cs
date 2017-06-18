using KursWebApplication.Data_Access;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Business_Logic
{
    public class DateLogic
    {
        DateDataAccess dataAccess = new DateDataAccess();

        public List<MyDBModels.DateWorkList> logicMethodForGetListData()
        {
            List<MyDBModels.DateWorkList> listData = dataAccess.getListData();
            return listData;
        }

        public MyDBModels.DateWorkList logicMethodForGetData(int id)
        {
            MyDBModels.DateWorkList data = dataAccess.getDataById(id);
            return data;
        }

        public void logicMethodForPostData(Models.DateWorkListModel data)
        {
            dataAccess.postDate(data);
        }

        public void logicMethodForDeleteDataint(int id)
        {
            dataAccess.deleteDate(id);
        }

        public MyDBModels.WorkList logicMethodForGetWorkListByDate(DateTime dateAction)
        {
            MyDBModels.WorkList data = dataAccess.getWorkListByDate(dateAction);
            return data;
        }

        
    }
}