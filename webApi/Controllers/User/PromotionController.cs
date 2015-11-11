using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Bll;

namespace webApi.Controllers.User
{
    public class PromotionController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<promotion_events> Get()
        {
            promotion_events_Bll eventBll=new promotion_events_Bll ();
            return eventBll.GetPromotionEventList(0);//类型
        }

        // GET api/<controller>/5
        public promotion_events Get(int id)
        {
            #region Bll
            Base_Bll bll = new Base_Bll();
            return bll.GetInfo<promotion_events>(id);
            #endregion
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}