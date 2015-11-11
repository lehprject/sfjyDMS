using System;
using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Idr_visit_Bll
    {
        #region  出诊相关 dr_visit

        #region 添加
        dr_visit CreateVisit(dr_visit info, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询

        List<dr_visit> SearchVisitList(int hospital_id, int drid, DateTime? visit_date1, DateTime? visit_date2, string visit_time,
           int service_type, int status, DateTime? createtime1, DateTime? createtime2,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        
        #endregion

        #endregion

        #region  预约相关 dr_pre_visit

        #region 添加
        dr_pre_visit CreatePreVisit(dr_pre_visit info, out string error);

        #endregion

        #region 修改

        #endregion

        #region 查询


        List<dr_pre_visit> SearchPreVisitList(int visit_id, int patient_id, DateTime? pre_date1, DateTime? pre_date2,
            string pre_time, int pre_type, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        List<dr_pre_visit> GetPreVisitListByVisitID(int visitID);


        #endregion

        #endregion
        
    
    }
}
