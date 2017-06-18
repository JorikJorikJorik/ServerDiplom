using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Data_Access
{
    public class RepairDataAccess
    {
        public List<FullRepairList> getFullListData()
        {
            var db = new MyDBModels.DB();

            List<FullRepairList> fullList = db.bus.Join(db.repairList, b => b.BusId, rep => rep.BusId, (b, rep) => new FullRepairList
            {
                Bus = new BusModel
                {
                    BusNumber = b.BusNumber,
                    Model = b.Model,
                    BusCondition = b.BusCondition
                },
                RepairBlank = new RepairListModel
                {
                    ServiceListId = rep.ServiceListId,
                    BusCondition = rep.BusCondition,
                    TimeGet = rep.TimeGet
                  }
            }).ToList();

            return fullList;
        }

        public List<FullRepairList> getFullListDataByUser(int number)
        {
            var db = new MyDBModels.DB();

            int idDriver = db.driver.Where(d => d.DriverNumber == number).FirstOrDefault().DriverId;
            List<MyDBModels.WorkList> work = db.workList.Where(d => d.DriverId == idDriver).ToList();
            List<FullRepairList> fullListJoin = db.bus.Join(db.repairList, b => b.BusId, rep => rep.BusId, (b, rep) => new FullRepairList
            {
                Bus = new BusModel
                {
                    BusId = b.BusId,
                    BusNumber = b.BusNumber,
                    Model = b.Model,
                    BusCondition = b.BusCondition

                },
                RepairBlank = new RepairListModel
                {
                    ServiceListId = rep.ServiceListId,
                    BusCondition = rep.BusCondition,
                    TimeGet = rep.TimeGet
                 }
            }).ToList();
            List<FullRepairList> fullListResult = new List<FullRepairList>();

            for (int i = 0; i < work.Count; i++)
            {
                fullListResult.AddRange(fullListJoin.Where(r => r.Bus.BusId == work[i].BusId && r.RepairBlank.TimeGet == work[i].DateAction).ToList());
            }
            return fullListResult;
        }


        public List<MyDBModels.RepairList> getListData()
        {
            var db = new MyDBModels.DB();
            List<MyDBModels.RepairList> listData = db.repairList.ToList();
            return listData;
        }

        public MyDBModels.RepairList getDataById(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.RepairList repairModel = db.repairList.Where(b => b.ServiceListId == id).FirstOrDefault();
            return repairModel;
        }

        public void postRepair(Models.RepairListModel value, int number)
        {

            var db = new MyDBModels.DB();
            int idDriver = db.driver.Where(d => d.DriverNumber == number).FirstOrDefault().DriverId;
            MyDBModels.WorkList work = db.workList.Where(d => d.DriverId == idDriver && d.DateAction == value.TimeGet).FirstOrDefault();

            MyDBModels.RepairList repairList = new MyDBModels.RepairList();
            repairList.BusId = work.BusId;
            repairList.BusCondition = value.BusCondition;
            repairList.TimeGet = value.TimeGet;
            db.repairList.Add(repairList);
            db.SaveChanges();
        }


        public void deleteRepair(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.RepairList repairList = db.repairList.Where(rl => rl.ServiceListId == id).FirstOrDefault();
            if (repairList != null)
            {
                db.repairList.Remove(repairList);
                db.SaveChanges();
            }
        }
    }
}