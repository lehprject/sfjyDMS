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
    public class PatientController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<patient_disease> Get(string name)
        {
            //分组查询
            string error = string.Empty;
            patient_info_Bll infobll = new patient_info_Bll();
            var resultlist = infobll.SearchPatientDiseaseList(0, DoctorID, name, null, null,null, null, null,null, out error);
            return resultlist;
        }

        // GET api/<controller>/5
        public patient_info Get(int id)
        {
            #region Bll
            Base_Bll bll = new Base_Bll();
            return bll.GetInfo<patient_info>(id);
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