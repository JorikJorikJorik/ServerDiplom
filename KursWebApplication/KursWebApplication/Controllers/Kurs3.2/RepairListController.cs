using KursWebApplication.Business_Logic;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace KursWebApplication.Controllers
{
    //[Authorize]
    public class RepairListController : ApiController
    {
      /*  RepairLogic logic = new RepairLogic();


        [System.Web.Http.Route("api/RepairList/Full")]
        [System.Web.Http.HttpGet]
        public List<FullRepairList> GetFull()
        {
            return logic.logicMethodForGetFullListData();
        }

        [System.Web.Http.Route("api/RepairList/Full/{number}")]
        [System.Web.Http.HttpGet]
        public List<FullRepairList> GetFullByUser(int number)
        {
            return logic.logicMethodForGetFullData(number);
        }

        // GET api/values
        [HttpGet]
        public List<MyDBModels.RepairList> Get()
        {
            return logic.logicMethodForGetListData();
        }

        // GET api/values/
        [HttpGet]
        public MyDBModels.RepairList Get(int id)
        {
            if (id != 0)
            {
                return logic.logicMethodForGetData(id);
            }
            return null;
        }

        // POST api/values
        [System.Web.Http.Route("api/RepairList/{number}")]
        [System.Web.Http.HttpPost]
        public int Post(Models.RepairListModel newGas, int number)
        {

            if (newGas != null && number > 0)
            {
                logic.logicMethodForPostData(newGas, number);

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
        [System.Web.Http.Route("api/RepairList/{id}")]
        [HttpDelete]
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
        */

    }
}