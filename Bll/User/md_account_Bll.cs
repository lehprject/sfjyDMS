using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBll;
using Dal;
using Model.ConfigClass;
using Share;

namespace Bll
{
    public class md_account_Bll : Imd_account_Bll
    {
        #region 添加

        /// <summary>
        /// 添加医生明细信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public md_dr_account CreateAccount(md_dr_account info, out string error)
        {
            error = string.Empty;
            try
            {
                md_account_DA da = new md_account_DA();
                da.CreateAccount(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error += Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }

        }

        /// <summary>
        /// 添加医生明细信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CreateAccountList(List<md_dr_account> list, out string error)
        {
            error = string.Empty;
            try
            {
                md_account_DA da = new md_account_DA();
                da.CreateAccountList(list, out error);
                return true;
            }
            catch (Exception ex)
            {
                error += Share.BaseTool.FormatExceptionMessage(ex);
                return false;
            }

        }
        #endregion
    }
}
