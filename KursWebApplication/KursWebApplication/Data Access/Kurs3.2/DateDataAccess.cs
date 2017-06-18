using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace KursWebApplication.Data_Access
{
    public class DateDataAccess
    {
        public List<MyDBModels.DateWorkList> getListData()
        {
            var db = new MyDBModels.DB();
            List<MyDBModels.DateWorkList> listData = db.dateWorkList.ToList();
            return listData;
        }

        public MyDBModels.DateWorkList getDataById(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.DateWorkList dateModel = db.dateWorkList.Where(b => b.DateId == id).FirstOrDefault();
            return dateModel;
        }

        public void postDate(Models.DateWorkListModel value)
        {
            var db = new MyDBModels.DB();
            MyDBModels.DateWorkList dateWorkList = new MyDBModels.DateWorkList();
            dateWorkList.DateAction = value.DateAction;
            dateWorkList.WorkListId = value.WorkListId;
            db.dateWorkList.Add(dateWorkList);
            db.SaveChanges();
        }

        public void deleteDate(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.DateWorkList dateWorkList = db.dateWorkList.Where(dwl => dwl.DateId == id).FirstOrDefault();
            if (dateWorkList != null)
            {
                db.dateWorkList.Remove(dateWorkList);
                db.SaveChanges();
            }
        }

        public MyDBModels.WorkList getWorkListByDate(DateTime dateAction)
        {
            var db = new MyDBModels.DB();
            DateTime dateTime1 = dateAction.Date;
            DateTime dateTime2 = dateAction.AddDays(1).Date;
            int workId = db.dateWorkList.Where(date => date.DateAction < dateTime2 && date.DateAction >= dateTime1).Select(x => x.WorkListId).FirstOrDefault();
            return db.workList.Where(dwl => dwl.WorkListId == workId).FirstOrDefault();

        }
    }
}