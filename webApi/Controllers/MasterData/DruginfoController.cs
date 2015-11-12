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
    public class DruginfoController :BaseApiController
    {

        // GET api/<controller>
        public IEnumerable<md_druginfo> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            //分组查询
            string error = string.Empty;
            md_druginfo_Bll drugbll = new md_druginfo_Bll();
            var resultlist = drugbll.SearchDruginfoList(null, null, 0, null, null,
            null, null,  pageindex.Value, PageSize, out error);
            return resultlist;
        }

        // GET api/<controller>/5
        public IEnumerable<md_druginfo> Get(string drugname, int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            //分组查询
            string error = string.Empty;
            md_druginfo_Bll drugbll = new md_druginfo_Bll();
            var resultlist = drugbll.SearchDruginfoList(drugname, null, 0, null, null,
            null, null, pageindex.Value, PageSize, out error);
            return resultlist;
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