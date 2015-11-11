using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBll;
using Dal;
using Model.ConfigClass;
using Share;

namespace Bll
{
    public class patient_order_Bll:Ipatient_order_Bll
    {
        #region 查询

        /// <summary>
        /// 对服务订单表表各个字段查询
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
        /// <param name="orderbyCol">排序字段,传入patient_order的字段,不需要前缀</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="error"></param>
        /// <returns>null则出错</returns>
        public List<patient_order> SearchPatientOrderList(int patient_id, int drid, int service_type, DateTime? createtime,
       orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                patient_order_DA da = new patient_order_DA();
                var resultList = da.SearchPatientOrderList(patient_id, drid, service_type, createtime,
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
    }
}
