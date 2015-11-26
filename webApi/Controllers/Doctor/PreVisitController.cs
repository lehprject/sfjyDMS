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
    public class PreVisitController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<dr_pre_visit> Get(int? pageindex, bool flag)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            dr_visit_Bll visitBll = new dr_visit_Bll();
            string error = string.Empty;
            var resultList = visitBll.SearchPreVisitList(DoctorID, 0, 0, null, null,
            null, 0, null, null, 0, null, null,
             null, null, pageindex.Value, PageSize, out error);
 

            return resultList;
        }

        // GET api/<controller>/5
        public List<dr_pre_visit> Get([FromBody] int visitid)
        {
            #region Bll
            string error = string.Empty;
            dr_visit_Bll visitBll = new dr_visit_Bll();
            var resultList = visitBll.GetPreVisitListByVisitID(visitid);
            #endregion

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