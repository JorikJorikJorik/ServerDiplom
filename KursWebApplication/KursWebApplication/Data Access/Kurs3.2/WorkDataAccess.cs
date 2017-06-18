using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace KursWebApplication.Data_Access
{
    public class WorkDataAccess
    {
        public List<FullWorkList> getListData()
        {
            var db = new MyDBModels.DB();

            List<FullWorkList> fullList = db.workList.Join(db.bus, w => w.BusId, b => b.BusId, (w, b) => new
            {
                WorkList = new WorkListModel {
                    WorkListId = w.WorkListId,
                    BusId = w.BusId,
                    DriverId = w.DriverId,
                    SecondNameDispatcher = w.SecondNameDispatcher,
                    DateAction = w.DateAction
                },

                Bus = new BusModel
                {
                    BusNumber = b.BusNumber,
                    Model = b.Model,
                    BusCondition = b.BusCondition
                },
               
            }).Join(db.driver,comb => comb.WorkList.DriverId, d => d.DriverId, (comb, d) => new FullWorkList {
                WorkList = new WorkListModel
                {
                    WorkListId = comb.WorkList.WorkListId,
                    BusId = comb.WorkList.BusId,
                    DriverId = comb.WorkList.DriverId,
                    SecondNameDispatcher = comb.WorkList.SecondNameDispatcher,
                    DateAction = comb.WorkList.DateAction
                },
                Bus = new BusModel
                {
                    BusNumber = comb.Bus.BusNumber,
                    Model = comb.Bus.Model,
                    BusCondition = comb.Bus.BusCondition
                },
                Driver = new DriverModel
                {
                    DriverNumber = d.DriverNumber,
                    Experience = d.Experience,
                    Qualification = d.Qualification,
                    Secondname = d.Secondname
                },
            }).ToList();
            
            return fullList;
        }

        public MyDBModels.WorkList getDataById(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.WorkList workModel = db.workList.Where(b => b.WorkListId == id).FirstOrDefault();
            return workModel;
        }

        public void postWork(Models.WorkListModel value)
        {
            var db = new MyDBModels.DB();
            MyDBModels.WorkList workList = new MyDBModels.WorkList();
            workList.DriverId = value.DriverId;
            workList.BusId = value.BusId;
            workList.SecondNameDispatcher = value.SecondNameDispatcher;
            workList.DateAction = value.DateAction;
            db.workList.Add(workList);


            MyDBModels.Bus busModel = db.bus.Where(b => b.BusId == value.BusId).FirstOrDefault();
            string title = "Bus: " + busModel.BusNumber.ToString() + "/" + busModel.Model;
            string info = "Date: " + value.DateAction + "\nDispatcher: " + value.SecondNameDispatcher;

            string number = EncryptClass.DESEncrypt(db.driver.Where(b => b.DriverId == value.DriverId).FirstOrDefault().DriverNumber.ToString());

            MyDBModels.Account accountModel = db.account.Where(b => b.NumberWorker == number).FirstOrDefault();
            string token = accountModel.Token;

           sendRequestToFirebase("\"" + title + "\"", "\"" + info + "\"", "\"" + token + "\"");

            db.SaveChanges();
        }


        public void deleteWork(int id)
        {
            var db = new MyDBModels.DB();
            MyDBModels.WorkList workList = db.workList.Where(wl => wl.WorkListId == id).FirstOrDefault();
            if (workList != null)
            {
                db.workList.Remove(workList);
                db.SaveChanges();
            }
        }

        public string sendRequestToFirebase(string title, string info, string token)
        {
            string result = "";
            string json = "{" +
                          "\"data\"" + " :{" +
                          "\"title\"" + ": " + title + "," +
                          "\"text\"" + ": " + info +
                          "}," +
                          "\"to\"" + ":" + token +
                          "}";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "key=AIzaSyAQD2wfyz55UVwHjhWEPoEUmKk1-T14Swo";
                result = client.UploadString("https://fcm.googleapis.com/fcm/send", "POST", json);
            }
            return result;


          }
         
    }
}