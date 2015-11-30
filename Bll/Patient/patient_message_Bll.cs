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


        public List<patient_message> SearchMessgaeList(int hispital_id,
             int patientid, int drid, int status, string contents, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                patient_message_DA messageDa = new patient_message_DA();

                var resultList = messageDa.SearchMessgaeList(hispital_id,patientid, drid, status, contents,createtime1, createtime2,
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

        #region  患者留言记录回复  patient_message_rcd
        #region 添加
        public patient_message_rcd CreateMessageRcd(patient_message_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                patient_message_DA messageDa = new patient_message_DA();
                info = messageDa.CreateMessageRcd(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }


        }
        #endregion

        #endregion
    }
}
