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
    public class RecipelistController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public patient_medical_rcd Get(int id)
        {
            Base_Bll bll = new Base_Bll();
            patient_medical_rcd info = bll.GetInfo<patient_medical_rcd>(id);
            return info;

        }

        // POST api/<controller>
        public Model.ResponseMessage Post([FromBody]patient_recipelist info, List<patient_recipelist_druguse> drugList)
        {
            patient_recipelist_Bll reciptBll = new patient_recipelist_Bll();
            string error = string.Empty;
            bool success = reciptBll.CreateReciptList(info, drugList, out error);

            Model.ResponseMessage result = new ResponseMessage();
            if(success)
            {
                result.bSuccess = true; 
            }
            else
            {
                result.bSuccess = false;
                result.Message = error;
            }

            return result;
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