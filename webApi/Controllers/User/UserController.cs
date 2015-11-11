using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Bll;
using Share;

namespace webApi.Controllers.User
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        public Model.ResponseMessage Get(string loginname, string loginpwd)
        {
            string error = string.Empty;
            string keyvalue = string.Empty;
            md_docter_Bll bll = new md_docter_Bll();

            Model.ResponseMessage result = new ResponseMessage();
            if (!string.IsNullOrEmpty(loginname) && !string.IsNullOrEmpty(loginpwd))
            {
                bll.LoginUtil(loginname, loginpwd, keyvalue, out error);

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

        public ResponseMessage Put(int id, string oldpwd, string newpwd)
        {
            string error = string.Empty;
            Base_Bll bll = new Base_Bll();
            md_docter info = bll.GetInfo<md_docter>(id);
            string encryptKey = string.Empty;
            var password = BaseTool.EncryptDES(oldpwd, encryptKey);

            Model.ResponseMessage result = new ResponseMessage();
            if (info.login_pwd == oldpwd)
            {
                var newpassword = BaseTool.EncryptDES(oldpwd, encryptKey);
                info.login_pwd = newpassword;
                info = new md_docter_Bll().UpdateDocter(info, out error);

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
    }
}
