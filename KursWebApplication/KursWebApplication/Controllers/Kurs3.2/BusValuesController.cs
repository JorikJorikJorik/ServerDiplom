

using KursWebApplication.Business_logic;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace KursWebApplication.Controllers
{
    //  [Authorize]
   /* public class BusValuesController : ApiController
    {
        BusLogic logic = new BusLogic();

        // GET api/values
        //  [HttpGet]
        public List<MyDBModels.Bus> Get()
        {
            return logic.logicMethodForGetListData();
        }

        // GET api/values/
        [System.Web.Http.HttpGet]
        public MyDBModels.Bus Get(int id)
        {
            if (id > 0)
            {
                return logic.logicMethodForGetData(id);
            }
            return null;
        }

        // POST api/values
        [System.Web.Http.HttpPost]
        public int Post(Models.BusModel newbus)
        {

            if (newbus != null)
            {
                logic.logicMethodForPostData(newbus);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
                return (int)response.StatusCode;
            }

            return (int)Request.CreateResponse(HttpStatusCode.BadRequest).StatusCode;

        }

        // DELETE api/values/
        [System.Web.Http.HttpDelete]
        public int Delete(int id)
        {
            if (id > 0)
            {
                logic.logicMethodForDeleteDataint(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
                return (int)response.StatusCode;
            }
            return (int)Request.CreateResponse(HttpStatusCode.BadRequest).StatusCode;
        }

        [System.Web.Http.Route("api/BusValues/Repair/{id}")]
        [System.Web.Http.HttpGet]
        public List<FullRepairList> GetRepairListById(int id)
        {
            if (id > 0)
            {
                return logic.logicMethodForGetRepairListById(id);
            }
            return null;
        }

        [System.Web.Http.Route("api/BusValues/Gas/{id}")]
        [System.Web.Http.HttpGet]
        public List<FullGasList> GetGasListById(int id)
        {
            if (id > 0)
            {
                return logic.logicMethodForGetGasListById(id);
            }
            return null;
        }
    }*/
}