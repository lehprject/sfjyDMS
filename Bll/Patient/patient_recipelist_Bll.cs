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
    public class patient_recipelist_Bll
    {
        #region  处方相关 patient_recipelist

        #region 添加
        public bool CreateReciptList(patient_medical_rcd info, List<patient_recipelist_druguse> drugList, out string error)
        {
            error = string.Empty;
            try
            {
                patient_recipelist_DA recipeDa = new patient_recipelist_DA();
                bool success = recipeDa.CreateReciptList(info, drugList, out error);
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


        public List<patient_recipelist> SearchReciptList(int medical_rcd_id, int hospital_id, int patient_id, string cust_name,
            DateTime? issue_time1, DateTime? issue_time2, DateTime? createtime1, DateTime? createtime2, string check_result, int fileid,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                patient_recipelist_DA recipeDa = new patient_recipelist_DA();
                var resultList = recipeDa.SearchReciptList(medical_rcd_id, hospital_id, patient_id, cust_name,
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

        #region  处方用药相关 patient_recipelist_druguse

        #region 添加
        public patient_medical_rcd Add(patient_medical_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                patient_recipelist_DA recipeDa = new patient_recipelist_DA();
                recipeDa.Add(info, out error);
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

        public List<patient_recipelist_druguse> GetRecipeDrugListByReciptID(int reciptID)
        {
            patient_recipelist_DA recipeDa = new patient_recipelist_DA();
            return recipeDa.GetRecipeDrugListByReciptID(reciptID);
        }

        #endregion

        #endregion
    }
}
