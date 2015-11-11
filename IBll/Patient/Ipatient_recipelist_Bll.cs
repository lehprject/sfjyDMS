using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ipatient_recipelist_Bll
    {
        #region  处方相关 patient_recipelist

        #region 添加
        bool CreateReciptList(patient_medical_rcd info, List<patient_recipelist_druguse> drugList, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询


        List<patient_recipelist> SearchReciptList(int medical_rcd_id, int hospital_id, int patient_id, string cust_name,
            DateTime? issue_time1, DateTime? issue_time2, DateTime? createtime1, DateTime? createtime2, string check_result, int fileid,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        #endregion

        #endregion

        #region  处方用药相关 patient_recipelist_druguse

        #region 添加
        patient_medical_rcd Add(patient_medical_rcd info, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询

        List<patient_recipelist_druguse> GetRecipeDrugListByReciptID(int reciptID);

        #endregion

        #endregion
    }
}
