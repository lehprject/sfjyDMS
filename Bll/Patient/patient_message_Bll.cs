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
    public class patient_message_Bll : Ipatient_message_Bll
    {
        #region  患者留言 patient_message

        #region 添加
        public patient_message CreateMessage(patient_message info, out string error)
        {
            error = string.Empty;
            try
            {
                patient_message_DA messageDa = new patient_message_DA();
                info = messageDa.CreateMessage(info, out error);
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


        public List<patient_message> SearchMessgaeList(
             int patientid, int drid, int status, string contents,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                patient_message_DA messageDa = new patient_message_DA();

                var resultList = messageDa.SearchMessgaeList(patientid,drid,status,contents,
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
    }
}
