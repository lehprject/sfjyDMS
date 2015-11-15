using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ipatient_druguse_suggest_Bll
    {
        #region  建议处方相关 patient_druguse_suggest

        #region 添加
        bool CreateDrugSuggest(patient_druguse_suggest info, List<patient_druguse_suggest_rcd> drugList, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询


        List<patient_druguse_suggest> SearchReciptList(int drid, int hospital_id, int patient_id, string cust_name,
            DateTime? issue_time1, DateTime? issue_time2, DateTime? createtime1, DateTime? createtime2, string check_result, int fileid,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        #endregion

        #endregion

        #region  建议用药相关 patient_druguse_suggest_rcd

        #region 添加
        patient_druguse_suggest_rcd AddDrugSuggestRcd(patient_druguse_suggest_rcd info, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询

        List<patient_druguse_suggest_rcd> GetDrugSuggestRcdBySuggestID(int reciptID);

        #endregion

        #endregion
    }
}
