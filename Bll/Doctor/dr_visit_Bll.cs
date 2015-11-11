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
    public class dr_visit_Bll : Idr_visit_Bll
    {
        #region  出诊相关 dr_visit

        #region 添加
        public dr_visit CreateVisit (dr_visit info, out string error)
        {
            error = string.Empty;
            try
            {
                dr_visit_DA da = new dr_visit_DA();
                da.CreateVisit(info,out error); 
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

        #endregion

        #region 查询

        /// <summary>
        /// 对出诊表各个字段查询
        /// </summary>
        /// <param name="hospital_id"></param>
        /// <param name="drid"></param>
        /// <param name="visit_date1"></param>
        /// <param name="visit_date2"></param>
        /// <param name="visit_time"></param>
        /// <param name="service_type"></param>
        /// <param name="status"></param>
        /// <param name="createtime1"></param>
        /// <param name="createtime2"></param>
        /// <param name="orderby">顺序 降序</param>
        /// <param name="orderbyCol">排序字段,传入dr_visit的字段,不需要前缀</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="error"></param>
        /// <returns>null则出错</returns>
        public List<dr_visit> SearchVisitList(int hospital_id, int drid, DateTime? visit_date1, DateTime? visit_date2, string visit_time,
            int service_type, int status, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                dr_visit_DA da = new dr_visit_DA();
                var resultList = da.SearchVisitList(  hospital_id,   drid,   visit_date1,  visit_date2,   visit_time,
                                    service_type,   status, createtime1,   createtime2,
                                    orderby,   orderbyCol,   pageIndex,   pageSize, out   error);
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

        #region  预约相关 dr_pre_visit

        #region 添加
        public dr_pre_visit CreatePreVisit(dr_pre_visit info, out string error)
        {
            error = string.Empty;
            try
            {
                dr_visit_DA da = new dr_visit_DA();
                da.CreatePreVisit(info, out error);
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

        #endregion

        #region 查询


        public List<dr_pre_visit> SearchPreVisitList(int visit_id, int patient_id, DateTime? pre_date1, DateTime? pre_date2,
            string pre_time, int pre_type, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                dr_visit_DA da = new dr_visit_DA();
                var resultList = da.SearchPreVisitList(visit_id, patient_id, pre_date1, pre_date2,
                                     pre_time, pre_type, createtime1, createtime2,
                                      orderby, orderbyCol, pageIndex, pageSize, out  error);
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
