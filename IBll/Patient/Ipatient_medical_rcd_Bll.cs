using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ipatient_medical_rcd_Bll
    {
        #region  出诊相关 patient_medical_rcd

        #region 添加
        patient_medical_rcd Add(patient_medical_rcd info, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询


        List<patient_medical_rcd> SearchVisitList(string code, int hospital_id, int drid, int patient_id, string cust_name,
            string rcd_result, DateTime? rcd_time1, DateTime? rcd_time2, DateTime? createtime1, DateTime? createtime2,
            int fileid, string next_chk_item, string next_visit_item,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        #endregion

        #endregion
    }
}
