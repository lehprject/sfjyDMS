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
    public class VisitController : BaseApiController
    {
        // GET api/<controller>
        public IEnumerable<dr_visit> Get(DateTime? visitdate,int? pageindex)
        {
            #region 参数
            if(pageindex.HasValue == false)
            {
                pageindex = 1;
            }
            #endregion

            #region Bll
            string error = string.Empty;
            dr_visit_Bll visitBll = new dr_visit_Bll();
            var resultList = visitBll.SearchVisitList(0, DoctorID, visitdate, visitdate, null, 0, 0, null, null, null, null, pageindex.Value, PageSize, out error);

            return resultList;
            #endregion 
        }

        // GET api/<controller>/5
        public dr_visit Get(int id)
        {
            #region Bll
            Base_Bll bll = new Base_Bll();
            return bll.GetInfo<dr_visit>(id);
            #endregion
        }

        // POST api/<controller>
        public void Post([FromBody]dr_visit value)
        {
            #region Bll
            dr_visit_Bll visitBll = new dr_visit_Bll();
            string error = string.Empty;
            visitBll.CreateVisit(value, out error); 
            #endregion

            if (string.IsNullOrEmpty(error))
            {

            }
            else
            {

            }
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