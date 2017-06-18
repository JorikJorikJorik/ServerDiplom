using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using KursWebApplication.Models;
using KursWebApplication.Business_Logic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KursWebApplication.Controllers
{
    //[Authorize]
    public class WorkListController : ApiController
    {
        /*WorkLogic logic = new WorkLogic();

        // GET api/values
        public List<FullWorkList> Get()
        {
            return logic.logicMethodForGetListData();
        }

        // GET api/values/
        public MyDBModels.WorkList Get(int id)
        {
            if (id != 0)
            {
                return logic.logicMethodForGetData(id);
            }
            return null;
        }

        // POST api/values
        public int Post(Models.WorkListModel newWork)
        {
            if (newWork != null)
            {
                logic.logicMethodForPostData(newWork);
                
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
        }*/
    }
}
