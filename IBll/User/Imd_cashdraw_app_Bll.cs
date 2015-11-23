using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Imd_cashdraw_app_Bll
    {
        #region 查询
        List<md_cashdraw_app> SearchCashdrawList(int drid, DateTime? app_time1, DateTime? app_time2, string opuser, DateTime? optime1, DateTime? optime2, int opstatus, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        List<md_cashdraw_app> GetCashdrawByIds(List<int> ids);

        #endregion

        #region 添加
        md_cashdraw_app CreateCashdrawApp(md_cashdraw_app info, out string error);
        #endregion

        #region 修改
        md_cashdraw_app UpdateCashdraw(md_cashdraw_app info, out string error);

        #endregion
    }
}
