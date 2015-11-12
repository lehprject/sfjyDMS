using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll;
using Model;
using Model.ConfigClass;

namespace webApi.Controllers.Patient
{
    public class ReciptDrugController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        //public IEnumerable<patient_recipelist_druguse> Get(int recipelist_id)
        //{
        //    patient_recipelist_Bll reciptBll = new patient_recipelist_Bll();
        //    var resultList = reciptBll.GetRecipeDrugListByReciptID(recipelist_id);
        //    return resultList;
        //}

        //public IEnumerable<string> GetHi()
        //{
        //    return new[] { "hi", "hello" };
        //}

        //public IEnumerable<string> Get(string p)
        //{
        //    return new[] { "hi", "hello",p };
        //}

        public IEnumerable<string> Get(DateTime d)
        {
            return new[] { "hi", "hello", "1" };
        }

        public IEnumerable<string> Get(DateTime d,int a)
        {
            return new[] { "hi", "hello", "2" };
        }


        public class vm_parameter
        {
            public int orderid { get; set; }

            public patient_recipelist_druguse[] jsondata { get; set; }
        }

        // POST api/<controller>
        public object Post1([FromBody]vm_parameter b, int orderid)
        {
            
            return b.jsondata;
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