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
    public class PreAddVisitController : BaseApiController 
    {
        public IEnumerable<dr_pre_addvisit> Get(int? pageindex, bool flag)
        {
            if (pageindex.HasValue == false)
                pageindex = 1;

            dr_addvisit_Bll visitBll = new dr_addvisit_Bll();
            string error = string.Empty;
            var resultList = visitBll.SearchPreAddVisitList(DoctorID, 0, 0, null, null,
            null, 0, null, null, 0, null, null,
             null, null, pageindex.Value, PageSize, out error);
            return resultList;
        }

        // GET api/<controller>/5
        public List<dr_pre_addvisit> Get([FromBody] int visitid)
        {
            #region Bll
            string error = string.Empty;
            dr_addvisit_Bll visitBll = new dr_addvisit_Bll();
            var resultList = visitBll.GetPreAddVisitListByVisitID(visitid);
            #endregion

            return resultList;
        }
    }
}