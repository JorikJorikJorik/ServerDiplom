using System.Web.Http;
using KursWebApplication.Models;
using KursWebApplication.Business_Logic;
using System.Collections.Generic;
using KursWebApplication.Business_Logic.Kurs3._2;

namespace KursWebApplication.Controllers
{
    public class ArduinoController : AccController
    {
        ArduinoLogic logic = new ArduinoLogic();

        [Route("api/Arduino")]
        [HttpGet]
        public ArduinoListModel getAllArduino()
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            return logic.getArduinoListLogic(authorization.getUserId());
        }

        [Route("api/Arduino")]
        [HttpPost]
        public void addArduino(ArduinoModel model)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.addArduinoLogic(authorization.getUserId(), model);
        }

        [Route("api/Arduino")]
        [HttpDelete]
        public void removeArduino(int arduinoId)
        {
            AuthorizationUtils authorization = new AuthorizationUtils(Request);
            logic.removeArduinoLogic(authorization.getUserId(), arduinoId);
        }

    }
}