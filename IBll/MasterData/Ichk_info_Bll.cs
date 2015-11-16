using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ichk_info_Bll
    {
        #region 检查单 chk_info 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        List<chk_info> SearchChkInfoList(
             int medical_id, int patient_id, int chk_type_id, int chk_demo_id, int fileid, int drid,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        #endregion

        #endregion

        #region 检查单类型 chk_type 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        List<chk_type> SearchChkTypeList(
             string name,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        #endregion

        #endregion

        #region 检查单模板 chk_demo 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        List<chk_demo> SearchChkTypeList(
             int typeid, string chk_item,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        #endregion

        #endregion
    }
}
