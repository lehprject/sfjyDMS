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
    public class SuggestController : BaseApiController
    {
        public IEnumerable<patient_druguse_suggest> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            patient_druguse_suggest_Bll suggestBll = new patient_druguse_suggest_Bll();
            string error = string.Empty;
            var resultList = suggestBll.SearchReciptList(DoctorID, 0, 0, null, null, null, null, null, null, 0,
             null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        // GET api/<controller>/5
        public patient_druguse_suggest Get(int id)
        {
            Base_Bll bll = new Base_Bll();
            patient_druguse_suggest info = bll.GetInfo<patient_druguse_suggest>(id);
            return info;

        }

        // POST api/<controller>
        public Model.ResponseMessage Post([FromBody]patient_druguse_suggest info, List<patient_druguse_suggest_rcd> drugList)
        {
            patient_druguse_suggest_Bll suggestBll = new patient_druguse_suggest_Bll();
            string error = string.Empty;
            bool success = suggestBll.CreateDrugSuggest(info, drugList, out error);

            Model.ResponseMessage result = new ResponseMessage();
            if (success)
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
    }
}