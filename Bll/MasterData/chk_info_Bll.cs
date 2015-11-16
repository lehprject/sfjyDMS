using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBll;
using Dal;
using Model;
using Model.ConfigClass;
using Share;

namespace Bll
{
    public class chk_info_Bll : Ichk_info_Bll
    {
        #region 检查单 chk_info 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        public List<chk_info> SearchChkInfoList(
             int medical_id, int patient_id, int chk_type_id, int chk_demo_id, int fileid, int drid,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                chk_info_DA chkDa = new chk_info_DA();
                var resultList = chkDa.SearchChkInfoList(
                                                   medical_id,   patient_id,   chk_type_id,   chk_demo_id,   fileid,drid,
                                                  orderby,   orderbyCol,   pageIndex,   pageSize, out   error);
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        public void SeachChkHistroy(int patient_id, int chk_type_id)
        {

        }
        #endregion

        #endregion

        #region 检查单类型 chk_type 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        public List<chk_type> SearchChkTypeList(
             string name,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                chk_info_DA chkDa = new chk_info_DA();
                var resultList = chkDa.SearchChkTypeList(
                                                   name,
                                                  orderby, orderbyCol, pageIndex, pageSize, out   error);
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }
        #endregion

        #endregion

        #region 检查单模板 chk_demo 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        public List<chk_demo> SearchChkTypeList(
             int typeid, string chk_item,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                chk_info_DA chkDa = new chk_info_DA();
                var resultList = chkDa.SearchChkTypeList(
                                                   typeid, chk_item,
                                                  orderby, orderbyCol, pageIndex, pageSize, out   error);
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }
        #endregion

        #endregion
    }
}
