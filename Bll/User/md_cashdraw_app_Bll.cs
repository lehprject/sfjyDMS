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
    public class md_cashdraw_app_Bll:Imd_cashdraw_app_Bll
    {
        #region 添加

        /// <summary>
        /// 添加提现申请信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public md_cashdraw_app CreateCashdrawApp(md_cashdraw_app info, out string error)
        {
            error = string.Empty;
            try
            {
                md_cashdraw_app_DA da = new md_cashdraw_app_DA();
                da.CreateCashdrawApp(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error += Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }

        }
        #endregion
    }
}
