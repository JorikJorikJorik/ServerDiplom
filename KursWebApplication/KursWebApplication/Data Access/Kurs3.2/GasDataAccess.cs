using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursWebApplication.Data_Access
{
    public class GasDataAccess
    {

        public List<FullGasList> getFullListData()
        {
            var db = new MyDBModels.DB();
    
                     List<FullGasList> fullList = db.bus.Join(db.gasList, b => b.BusId, gas => gas.BusId, (b,gas) => new FullGasList{
                       Bus = new BusModel { 
                             BusNumber = b.BusNumber,
                             Model = b.Model,
                            },
                         GasBlank = new GasListModel {
                             GasListId = gas.GasListId,
                             CostGas = gas.CostGas,
                             CountLitre = gas.CountLitre,
                             TypeGas = gas.TypeGas,
                             TimeGet = gas.TimeGet
                            } 
               }).ToList();
            
            return fullList;
        }

        public List<FullGasList> getFullListDataByUser(int number)
        {
            var db = new MyDBModels.DB();

            int idDriver = db.driver.Where(d => d.DriverNumber == number).FirstOrDefault().DriverId;
            List<MyDBModels.WorkList> work = db.workList.Where(d => d.DriverId == idDriver).ToList();
            List<FullGasList> fullListJoin = db.bus.Join(db.gasList, b => b.BusId, gas => gas.BusId,  (b, gas) => new FullGasList
            {
                Bus = new BusModel
                {
                    BusId = b.BusId,
                    BusNumber = b.BusNumber,
                    Model = b.Model,
                },
                GasBlank = new GasListModel
                {
                    GasListId = gas.GasListId,
                    CostGas = gas.CostGas,
                    CountLitre = gas.CountLitre,
                    TypeGas = gas.TypeGas,
                    TimeGet = gas.TimeGet
                }
            }).ToList();
            List<FullGasList> fullListResult = new List<FullGasList>();

            for (int i = 0; i< work.Count; i++) {
                fullListResult.AddRange(fullListJoin.Where(g => g.Bus.BusId == work[i].BusId && g.GasBlank.TimeGet == work[i].DateAction).ToList());
            }
            return fullListResult;
        }

        public List<MyDBModels.GasList> getListData()
        {
            var db = new MyDBModels.DB();
            List<MyDBModels.GasList> listData = db.gasList.ToList();
            return listData;
        }


        public MyDBModels.GasList getDataById(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.GasList gasModel = db.gasList.Where(b => b.GasListId == id).FirstOrDefault();
            return gasModel;
        }

        public void postGas(Models.GasListModel value, int number)
        {
            var db = new MyDBModels.DB();
            int idDriver = db.driver.Where(d => d.DriverNumber == number).FirstOrDefault().DriverId;
            MyDBModels.WorkList work = db.workList.Where(d => d.DriverId == idDriver && d.DateAction == value.TimeGet).FirstOrDefault();


            MyDBModels.GasList gasList = new MyDBModels.GasList();
            gasList.BusId = work.BusId;
            gasList.CostGas = value.CostGas;
            gasList.CountLitre = value.CountLitre;
            gasList.TimeGet = value.TimeGet;
            gasList.TypeGas = value.TypeGas;
            db.gasList.Add(gasList);
            db.SaveChanges();
        }


        public void deleteGas(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.GasList gasList = db.gasList.Where(gl => gl.GasListId == id).FirstOrDefault();
            if (gasList != null)
            {
                db.gasList.Remove(gasList);
                db.SaveChanges();
            }
        }
    }
}