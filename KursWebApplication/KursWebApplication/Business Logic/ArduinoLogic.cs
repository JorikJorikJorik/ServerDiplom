using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KursWebApplication.Data_Access;
using KursWebApplication.Models;

namespace KursWebApplication.Business_Logic
{
    public class ArduinoLogic
    {
        ArduinoDataAccess dataAccesss = new ArduinoDataAccess();

        public ArduinoListModel getArduinoListLogic(int userId)
        {
            return dataAccesss.getArduinoList(userId);
        }

        public void addArduinoLogic(int userId, ArduinoModel model)
        {
             dataAccesss.addArduino(userId, model);
        }

        public void removeArduinoLogic(int userId, int arduinoId)
        {
             dataAccesss.removeArduino(userId, arduinoId);
        }
    }
}