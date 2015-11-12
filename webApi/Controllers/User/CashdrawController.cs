using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Bll;

namespace webApi.Controllers.User
{
    public class CashdrawController : ApiController
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

        public Model.ResponseMessage Post([FromBody]md_cashdraw_app info)
        {
            md_cashdraw_app_Bll cashdrawBll = new md_cashdraw_app_Bll();
            string error = string.Empty;
            cashdrawBll.CreateCashdrawApp(info, out error);

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