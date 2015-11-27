using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBll;
using Dal;

namespace Bll
{
    public class md_docter_Bll : Imd_docter_Bll
    {
        #region md_docter
        #region 查询

        /// <summary>
        /// 查询单个医生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public md_docter GetDocterById(int id)
        {
            return new md_docter_DA().GetDocterById(id);
        }

        /// <summary>
        /// 用户登录查询
        /// </summary>
        /// <param name="loginname">账号</param>
        /// <param name="loginpass">密码</param>
        /// <returns></returns>
        public md_docter LoginUtil(string loginname, string loginpass, string keyvalue, out string error)
        {
            error = string.Empty;
            try
            {
                string Encryptpass = Share.BaseTool.EncryptDES(loginpass, keyvalue);

                if (Encryptpass == loginpass)
                {
                    error = "用户验证失败";
                    return null;
                }
                md_docter info = new md_docter_DA().LoginUtil(loginname, Encryptpass);
                if (info == null)
                {
                    error = "登录账号或密码错误";
                    return null;
                }
                return info;

            }
            catch (Exception ex)
            {
                error = "登录验证失败，请联系IT部";
                return null;
            }
        }

        /// <summary>
        /// 根据医生id集合查询
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<md_docter> GetDoctorByIds(List<string> ids)
        {
            return new md_docter_DA().GetDoctorByIds(ids);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改医生信息
        /// </summary>
        /// <param name="info">医生实体</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public md_docter UpdateDocter(md_docter info, out string error)
        {
            error = string.Empty;
            try
            {
                md_docter_DA da = new md_docter_DA();
                return da.UpdateDocter(info, out error);
            }
            catch (Exception ex)
            {
                error = "修改失败";
                return null;
            }
        }

        public bool UpdateChashdrawList(List<md_docter> list, out string error)
        {
            error = string.Empty;
            try
            {
                md_docter_DA da = new md_docter_DA();
                return da.UpdateChashdrawList(list, out error);
            }
            catch (Exception ex)
            {
                error = "修改失败";
                return false;
            }
        }

        #endregion
        #endregion
    }
}
