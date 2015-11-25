using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll;
using Model;
using Share;

namespace webApi.Controllers.Doctor
{
    public class RecallRcdController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public ResponseMessage Post([FromBody]string value)
        {
            dr_recall_rcd_Bll recallBll = new dr_recall_rcd_Bll();
            string error = string.Empty;

            dr_recall_rcd recallrcd = new dr_recall_rcd();
            recallrcd.Initial();
            recallrcd.drid = DoctorID;
            recallBll.CreateDrRecallRcd(recallrcd, out error);

            Model.ResponseMessage result = new ResponseMessage();
            if (!string.IsNullOrEmpty(error))
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