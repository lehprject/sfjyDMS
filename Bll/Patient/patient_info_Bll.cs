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
    public class patient_info_Bll
    {
        #region 患者病种
        #region 查询
        /// <summary>
        /// 分组查询患者病种信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="gender"></param>
        /// <param name="cardtype"></param>
        /// <param name="cardno"></param>
        /// <param name="createtime"></param>
        /// <param name="alllergic_his"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<patient_disease> SearchPatientDiseaseList(string name, string mobile, string gender, string cardtype, string cardno, DateTime? createtime,
           string alllergic_his, out string error)
        {
            error = string.Empty;
            try
            {
                patient_info_DA da = new patient_info_DA();
                var resultList = da.SearchPatientDiseaseList(name, mobile, gender, cardtype, cardno, createtime,alllergic_his, out error);
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
