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
        #region 查询

        public List<md_cashdraw_app> SearchCashdrawList(int drid, DateTime? app_time1, DateTime? app_time2, string opuser, DateTime? optime1, DateTime? optime2, int opstatus, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            record = 0;
            error = string.Empty;
            try
            {
                md_cashdraw_app_DA da = new md_cashdraw_app_DA();
                var resultList = da.SearchCashdrawList(drid, app_time1, app_time2, opuser, optime1, optime2, opstatus, orderby, orderbyCol, pageIndex, pageSize, out record,out error);
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        public List<md_cashdraw_app> GetCashdrawByIds(List<int> ids)
        {
            return new md_cashdraw_app_DA().GetCashdrawByIds(ids);
        }

        #endregion
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

        #region 修改

        public md_cashdraw_app UpdateCashdraw(md_cashdraw_app info, out string error)
        {
            error = string.Empty;
            try
            {
                md_cashdraw_app_DA da = new md_cashdraw_app_DA();
                da.UpdateCashdraw(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        #endregion
    }
}
