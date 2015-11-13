using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Bll;

namespace webApi.Controllers.Patient
{
    public class OrderController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<patient_order> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            patient_order_Bll Bll = new patient_order_Bll();
            string error = string.Empty;
            int service_type = 0;
            var resultList = Bll.SearchPatientOrderList(0, DoctorID, service_type, null,null,
       null, null, pageindex.Value, PageSize, out  error);
            return resultList;
        }

        // GET api/<controller>/5
        public IEnumerable<patient_order> Get(int patient_id, int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            patient_order_Bll Bll = new patient_order_Bll();
            string error = string.Empty;
            int service_type = 0;
            var resultList = Bll.SearchPatientOrderList(0, DoctorID, service_type, null,null,
       null, null, pageindex.Value, PageSize, out  error);
            return resultList;
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