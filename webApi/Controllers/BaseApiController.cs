using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApi.Controllers
{
    public class BaseApiController : ApiController
    {
        #region 用户信息
        protected int DoctorID { get; set; }

        #endregion

        #region 配置信息
        protected int PageSize { get { return 20; }  }
        #endregion


    }
}