using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Idr_addvisit_Bll
    {
        #region  加号相关 dr_addvisit

        #region 添加
        dr_addvisit CreateAddVisit(dr_addvisit info, out string error);
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
        /// <param name="orderbyCol">排序字段,传入dr_addvisit的字段,不需要前缀</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="error"></param>
        /// <returns>null则出错</returns>
        List<dr_addvisit> SearchAddVisitList(int hospital_id, int drid, DateTime? visit_date1, DateTime? visit_date2, string visit_time,
            int service_type, int status, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        #endregion

        #endregion

        #region  预约加号相关 dr_pre_addvisit

        #region 添加
        dr_pre_addvisit CreatePreAddVisit(dr_pre_addvisit info, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询


        List<dr_pre_addvisit> SearchPreAddVisitList(int drid, int visit_id, int patient_id, DateTime? pre_date1, DateTime? pre_date2,
            string pre_time, int pre_type, DateTime? createtime1, DateTime? createtime2, int status, DateTime? visit_date1, DateTime? visit_date2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        List<dr_pre_addvisit> GetPreAddVisitListByVisitID(int visitID);


        #endregion

        #endregion
    }
}
