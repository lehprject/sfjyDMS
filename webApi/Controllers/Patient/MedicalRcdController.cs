﻿using System;
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
    public class MedicalRcdController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<patient_medical_rcd> Get(int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            patient_medical_rcd_Bll medicalBll = new patient_medical_rcd_Bll();
            string error = string.Empty;
            var resultList = medicalBll.SearchMedicalList(null, 0, DoctorID, 0, null, null,
                null, null, null, null, 0,
                null, null, null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        // GET api/<controller>/5
        public IEnumerable<patient_medical_rcd> Get(int patient_id, int? pageindex)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            patient_medical_rcd_Bll medicalBll = new patient_medical_rcd_Bll();
            string error = string.Empty;
            var resultList = medicalBll.SearchMedicalList(null, 0, 0, patient_id, null, null,
                null, null, null, null, 0,
                null, null, null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        // POST api/<controller>
        public Model.ResponseMessage Post([FromBody]patient_medical_rcd value)
        {
            value.createtime = DateTime.Now;
            value.drid = DoctorID;

            patient_medical_rcd_Bll medicalBll = new patient_medical_rcd_Bll();
            string error = string.Empty;
            value = medicalBll.Add(value, out error);

            Model.ResponseMessage result = new ResponseMessage();

            if (string.IsNullOrEmpty(error))
            {
                result.bSuccess = true;
                result.Extend1 = value.pkid.ToString();
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