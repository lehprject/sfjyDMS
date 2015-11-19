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
    public class PromotionController :BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<promotion_events> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            promotion_events_Bll Bll = new promotion_events_Bll();
            string error = string.Empty;
            var resultList = Bll.GetPromotionEventList(0, 0, null, null, null, null, null, null, null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        // GET api/<controller>/5
        public IEnumerable<promotion_events> Get(int patient_id, int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            promotion_events_Bll Bll = new promotion_events_Bll();
            string error = string.Empty;
            var resultList = Bll.GetPromotionEventList(0,0, null, null, null, null, null, null, null, null, pageindex.Value, PageSize, out error);
            return resultList;
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