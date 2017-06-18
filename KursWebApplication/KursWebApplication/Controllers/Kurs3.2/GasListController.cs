using KursWebApplication.Business_Logic;
using KursWebApplication.Models;
using System;
using System.Collections.Generic;
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
  /*  public class GasListController : ApiController
    {
        GasLogic logic = new GasLogic();


        [System.Web.Http.Route("api/GasList/Full")]
        [System.Web.Http.HttpGet]
        public List<FullGasList> GetFull()
        {
            return logic.logicMethodForFullGetListData();
        }

        [System.Web.Http.Route("api/GasList/Full/{number}")]
        [System.Web.Http.HttpGet]
        public List<FullGasList> GetFullByUser(int number)
        {
            return logic.logicMethodForFullGetListDataByUser(number);
        }


        // GET api/values
        [System.Web.Http.HttpGet]
        public List<MyDBModels.GasList> Get()
        {
            return logic.logicMethodForGetListData();
        }

        // GET api/values/
        [System.Web.Http.HttpGet]
        public MyDBModels.GasList Get(int id)
        {
            if (id > 0)
            {
                return logic.logicMethodForGetData(id);
            }
            return null;
        }

        // POST api/values
        [System.Web.Http.Route("api/GasList/{number}")]
        [System.Web.Http.HttpPost]
        public int Post(Models.GasListModel newGas, int number)
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
        [System.Web.Http.Route("api/GasList/{id}")]
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
    }*/
}