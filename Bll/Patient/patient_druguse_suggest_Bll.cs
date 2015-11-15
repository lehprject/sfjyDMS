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
    public class patient_druguse_suggest_Bll : Ipatient_druguse_suggest_Bll
    {
        #region  建议处方相关 patient_druguse_suggest

        #region 添加
        public bool CreateDrugSuggest(patient_druguse_suggest info, List<patient_druguse_suggest_rcd> drugList, out string error)
        {
            error = string.Empty;
            try
            {
                patient_druguse_suggest_DA suggestDa = new patient_druguse_suggest_DA();
                bool success = suggestDa.CreateDrugSuggest(info, drugList, out error);
                return success;

            }
            catch (Exception ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return false;
            }

        }
        #endregion

        #region 修改

        #endregion

        #region 查询


        public List<patient_druguse_suggest> SearchReciptList(int drid, int hospital_id, int patient_id, string cust_name,
            DateTime? issue_time1, DateTime? issue_time2, DateTime? createtime1, DateTime? createtime2, string check_result, int fileid,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                patient_druguse_suggest_DA suggestDa = new patient_druguse_suggest_DA();
                var resultList = suggestDa.SearchReciptList(drid, hospital_id, patient_id, cust_name,
                                                            issue_time1, issue_time2, createtime1, createtime2, check_result, fileid,
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

        #region  建议用药相关 patient_druguse_suggest_rcd

        #region 添加
        public patient_druguse_suggest_rcd AddDrugSuggestRcd(patient_druguse_suggest_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                patient_druguse_suggest_DA suggestDa = new patient_druguse_suggest_DA();
                suggestDa.AddDrugSuggestRcd(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }

        }
        #endregion

        #region 修改

        #endregion

        #region 查询

        public List<patient_druguse_suggest_rcd> GetDrugSuggestRcdBySuggestID(int reciptID)
        {
            patient_druguse_suggest_DA suggestDa = new patient_druguse_suggest_DA();
            return suggestDa.GetDrugSuggestRcdBySuggestID(reciptID);
        }

        #endregion

        #endregion
    }
}
