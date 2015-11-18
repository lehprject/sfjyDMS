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
    public class UserController : BaseApiController
    {
        public string Get()
        {
            return "fdfd";
        }

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
                    result.Message = "登录成功";
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

        // PUT api/< >/5
        public ResponseMessage Put(int id, [FromBody]md_docter value)
        {
            Model.ResponseMessage result = new ResponseMessage();
            string error = string.Empty;

            Base_Bll bll = new Base_Bll();
            var info = bll.GetInfo<md_docter>(DoctorID);
            info.login_pwd=value.oldpwd;
            md_docter_Bll docbll = new md_docter_Bll();
            docbll.UpdateDocter(info, out error);
            if (string.IsNullOrEmpty(error))
            {
                result.bSuccess = true;
                result.Message = "修改成功";
            }
            else
            {
                result.bSuccess = false;
                result.Message = error;
            }

            return result;
            
        }

        public ResponseMessage Post([FromBody]md_docter value)
        {
            Model.ResponseMessage result = new ResponseMessage();
            try
            {
                string error = string.Empty;
                Base_Bll bll = new Base_Bll();
                md_docter info = bll.GetInfo<md_docter>(DoctorID);
                string encryptKey = string.Empty;
                var password = BaseTool.EncryptDES(value.oldpwd, encryptKey);

                if (info.login_pwd == password)
                {
                    var newpassword = BaseTool.EncryptDES(value.login_pwd, encryptKey);
                    info.login_pwd = newpassword;
                    info = new md_docter_Bll().UpdateDocter(info, out error);

                    if (string.IsNullOrEmpty(error))
                    {
                        result.bSuccess = true;
                        result.Message = "修改成功";
                    }
                    else
                    {
                        result.bSuccess = false;
                        result.Message = error;
                    }
                }
            }
            catch(Exception ex)
            {
                result.bSuccess=false;
            }
            return result;
        }
    }
}
