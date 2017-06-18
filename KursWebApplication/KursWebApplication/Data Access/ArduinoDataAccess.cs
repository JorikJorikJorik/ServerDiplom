using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Models;

namespace KursWebApplication.Data_Access
{
    public class ArduinoDataAccess
    {

        public ArduinoListModel getArduinoList(int userId)
        {
            var db = new MyDBModels.DB();
            List<ArduinoModel> arduinoArray = new List<ArduinoModel>();
            var arduinoArrayId = arduinoArrayByUserId(db, userId);

            foreach (int arduinoId in arduinoArrayId)
            {
                var arduinoDbModel = db.arduino.Where(a => a.ArduinoId == arduinoId).First();
                arduinoArray.Add(createArduinoModelFromDB(arduinoDbModel));
            }

            ArduinoListModel list = new ArduinoListModel();
            list.ArduinoList = arduinoArray;
            return list;
        }

        public void addArduino(int userId, ArduinoModel model)
        {
            var db = new MyDBModels.DB();

            MyDBModels.Arduino arduinoModel = new MyDBModels.Arduino();
            arduinoModel.Name = model.Name;
            arduinoModel.Mac = model.Mac;

            db.arduino.Add(arduinoModel);
            db.SaveChanges();

            var arduinoArrayId = db.user.Where(u => u.UserId == userId).First().ArduinoIdArray.ToList();
            arduinoArrayId.Add(db.arduino.OrderByDescending(a => a.ArduinoId).FirstOrDefault().ArduinoId);
          
            db.user.Where(u => u.UserId == userId).First().ArduinoIdArray = arduinoArrayId.ToArray();
            db.SaveChanges();
        }

        public void removeArduino(int userId, int ardinoId)
        {
            var db = new MyDBModels.DB();

            var arduinoArrayId = arduinoArrayByUserId(db, userId).ToList();
            arduinoArrayId.Remove(ardinoId);
            db.user.Where(u => u.UserId == userId).First().ArduinoIdArray = arduinoArrayId.ToArray();

            var arduinoDbModel = db.arduino.Where(a => a.ArduinoId == ardinoId).First();
            db.arduino.Remove(arduinoDbModel);

            db.SaveChanges();
        }

        private ArduinoModel createArduinoModelFromDB(MyDBModels.Arduino arduino)
        {
            ArduinoModel model = new ArduinoModel();

            model.ArduinoId = arduino.ArduinoId;
            model.Name = arduino.Name;
            model.Mac = arduino.Mac;

            return model;
        }

        private int[] arduinoArrayByUserId(MyDBModels.DB db, int userId)
        {
            return db.user.Where(u => u.UserId == userId).First().ArduinoIdArray;
        }

    }
}