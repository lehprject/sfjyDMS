using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll;
using Model;
using Model.ConfigClass;
using Share;

namespace webApi.Controllers.Patient
{
    public class RecipelistController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<patient_recipelist> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            patient_recipelist_Bll recipeBll = new patient_recipelist_Bll();
            string error = string.Empty;
            var resultList = recipeBll.SearchReciptList(DoctorID, 0, 0, 0, null, null, null, null, null, null, 0,
             null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        /// <summary>
        /// 查询处方详细信息及处方列表信息
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public patient_recipelist Get(int recipeId)
        {
            Base_Bll bll = new Base_Bll();
            patient_recipelist info = bll.GetInfo<patient_recipelist>(recipeId);
            bll.GetFor(info, t => new { t.patient_name,t.gender,t.birth,t.alllergic_his });

            List<patient_recipelist_druguse> druguselist = new patient_recipelist_Bll().GetRecipeDrugListByReciptID(recipeId);
            bll.GetFor(druguselist, t => new { t.commonname, t.standard });

            info.druguselist = druguselist;

            if (info.birth.Year != Share.BaseTool.MiniDate.Year)
            {
                int age = DateTime.Now.Year - info.birth.Year;
                if (DateTime.Now < info.birth.AddYears(age)) age--;
                info.patient_age = age;
            }
            patient_address address = new patient_info_Bll().GetDefaultPatientAddressByPatientId(info.patient_id);
            info.address = address.contact_add;
            return info;
        }

        // GET api/<controller>/5
        public patient_medical_rcd Get(int id,bool flag)
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