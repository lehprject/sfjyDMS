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
    public class MasterDataController : ApiController
    {
        #region 医院
        // GET api/<controller>
        public IEnumerable<md_hospital> Get()
        {
            md_somebase_info_Bll basebll = new md_somebase_info_Bll();
            return basebll.GetAllHospitalList();
        }
        #endregion

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