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
    public class patient_medical_rcd_Bll : Ipatient_medical_rcd_Bll
    {
        #region  出诊相关 patient_medical_rcd

        #region 添加
        public patient_medical_rcd Add(patient_medical_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                patient_medical_rcd_DA medicalDa = new patient_medical_rcd_DA();
                info = medicalDa.Add(info, out error);
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


        public List<patient_medical_rcd> SearchVisitList(string code, int hospital_id, int drid, int patient_id, string cust_name,
            string rcd_result, DateTime? rcd_time1, DateTime? rcd_time2, DateTime? createtime1, DateTime? createtime2,
            int fileid, string next_chk_item, string next_visit_item,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                patient_medical_rcd_DA medicalDa = new patient_medical_rcd_DA();

                var resultList = medicalDa.SearchVisitList( code,  hospital_id,  drid,  patient_id,  cust_name,
                                                 rcd_result, rcd_time1,  rcd_time2,  createtime1,createtime2,
                                                 fileid,  next_chk_item,  next_visit_item,
                                                  orderby,  orderbyCol,  pageIndex,  pageSize, out  error);
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
