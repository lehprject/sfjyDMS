using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Bll;

namespace webApi.Controllers.MasterData
{
    public class ChkinfoController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<controller>/5
        public IEnumerable<chk_info> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            chk_info_Bll chkBll = new chk_info_Bll();
            string error = string.Empty;
            var resultList = chkBll.SearchChkInfoList(
             0, 0, 0, 0, 0, DoctorID,
            null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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