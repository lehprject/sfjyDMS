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
    public class UserController : ApiController
    {
        // GET api/<controller>
        public Model.ResponseMessage Get(string loginname, string loginpwd)
        {
            string error = string.Empty;
            string keyvalue=string.Empty;
            md_docter_Bll bll = new md_docter_Bll();

            Model.ResponseMessage result = new ResponseMessage();
            if(!string.IsNullOrEmpty(loginname)&&!string.IsNullOrEmpty(loginpwd))
            {
                bll.LoginUtil(loginname, loginpwd,keyvalue,out error);
       
                if (string.IsNullOrEmpty(error))
                {
                    result.bSuccess = true;
                }
                else
                {
                    result.bSuccess = false;
                    result.Message = error;
                }            
            }
            return result;
        }


        // GET api/<controller>/5
        public md_docter Get(int id)
        {
            #region Bll
            Base_Bll bll = new Base_Bll();
            return bll.GetInfo<md_docter>(id);
            #endregion
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]md_docter value)
        {
            string error = string.Empty;
            string keyvalue = string.Empty;
            md_docter_Bll bll = new md_docter_Bll();
            bll.UpdateDocter(value, out error);
        }
    }
}
