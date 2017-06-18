using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Data_Access
{
    public class BusDataAccess
    {
        public List<MyDBModels.Bus> getListData()
        {
            var db = new MyDBModels.DB();
            List<MyDBModels.Bus> listData = db.bus.ToList();
            return listData;
        }

        public MyDBModels.Bus getDataById(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Bus busModel = db.bus.Where(b => b.BusId == id).FirstOrDefault();
            return busModel;
        }

        public void postBus(Models.BusModel newbus)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Bus bus = new MyDBModels.Bus();
            bus.BusCondition = newbus.BusCondition;
            bus.BusNumber = newbus.BusNumber;
            bus.Model = newbus.Model;
            db.bus.Add(bus);
            db.SaveChanges();

        }


        public void deleteBus(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.Bus bus = db.bus.Where(b => b.BusId == id).FirstOrDefault();
            if (bus != null)
            {
                db.bus.Remove(bus);
                db.SaveChanges();
            }
        }

        public List<FullRepairList> getRepairListData(int id)
        {
            var db = new MyDBModels.DB();
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
                    BusId = b.BusId,
                    ServiceListId = rep.ServiceListId,
                    BusCondition = rep.BusCondition,
                    TimeGet = rep.TimeGet
                }
            }).ToList();
            List<FullRepairList> fullListResult = new List<FullRepairList>();
            fullListResult.AddRange(fullListJoin.Where(r => r.RepairBlank.BusId == id).ToList());
            return fullListResult;
        }

    
        public List<FullGasList> getGasListData(int id)
        {
            var db = new MyDBModels.DB();
            List<FullGasList> fullListJoin = db.bus.Join(db.gasList, b => b.BusId, gas => gas.BusId, (b, gas) => new FullGasList
            {
                Bus = new BusModel
                {
                    BusId = b.BusId,
                    BusNumber = b.BusNumber,
                    Model = b.Model,
                },
                GasBlank = new GasListModel
                {
                    BusId = gas.BusId,
                    GasListId = gas.GasListId,
                    CostGas = gas.CostGas,
                    CountLitre = gas.CountLitre,
                    TypeGas = gas.TypeGas,
                    TimeGet = gas.TimeGet
                }
            }).ToList();
            List<FullGasList> fullListResult = new List<FullGasList>();
            fullListResult.AddRange(fullListJoin.Where(g => g.GasBlank.BusId == id).ToList());
            return fullListResult;
        }
    }
}