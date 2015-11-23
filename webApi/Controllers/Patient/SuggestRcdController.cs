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
    public class SuggestRcdController : BaseApiController
    {
        #region 测试
        public string GetMyName()
        {
            return "Charles";
        }

        public string Get()
        {
            return "SuggestRcdController:Get()";
        }

        public string Get(int id)
        {
            return "SuggestRcdController:Get(int id):" + id.ToString() ;
        }

        //与Get(int id) 冲突
        //public string Get2(int id)
        //{
        //    return "SuggestRcdController:Get2(int id):" + id.ToString();
        //}

        public string Post(int id)
        {
            return "SuggestRcdController:Post(int id):" + id.ToString();
        }

        #region 2个参数
        //public string GetTwoP(int p1, int p2)
        //{
        //    return "GetTwoP(int p1, int p2)";
        //}

        public string GetTwoP(int p1,int p2,int? p3)
        {
            // 不 与GetTwoP(int p1, int p2)冲突！！
            return "GetTwoP(int p1,int p2,int? p3)";
        }
        #endregion

        #endregion
    }
}