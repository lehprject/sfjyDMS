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
    public class AddVisitController : BaseApiController
    {
        public IEnumerable<dr_addvisit> Get(DateTime? visitdate, int? pageindex)
        {
            #region 参数
            if (pageindex.HasValue == false)
            {
                pageindex = 1;
            }
            #endregion

            #region Bll
            string error = string.Empty;
            dr_addvisit_Bll visitBll = new dr_addvisit_Bll();
            var resultList = visitBll.SearchAddVisitList(0, DoctorID, visitdate, visitdate, null, 0, 0, null, null, null, null, pageindex.Value, PageSize, out error);

            return resultList;
            #endregion
        }

        // GET api/<controller>/5
        public dr_addvisit Get(int id)
        {
            #region Bll
            Base_Bll bll = new Base_Bll();
            return bll.GetInfo<dr_addvisit>(id);
            #endregion
        }

        // POST api/<controller>
        public Model.ResponseMessage Post([FromBody]dr_addvisit value)
        {
            #region Bll
            dr_addvisit_Bll visitBll = new dr_addvisit_Bll();
            string error = string.Empty;
            visitBll.CreateAddVisit(value, out error);
            #endregion

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
    }
}