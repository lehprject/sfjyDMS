using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ipatient_message_Bll
    {
        #region  患者留言 patient_message

        #region 添加
        patient_message CreateMessage(patient_message info, out string error);
        #endregion

        #region 修改

        #endregion

        #region 查询


        List<patient_message> SearchMessgaeList(int hispital_id,
             int patientid, int drid, int status, string contents, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        #endregion

        #endregion

        #region  患者留言记录回复  patient_message_rcd
        #region 添加
        patient_message_rcd CreateMessageRcd(patient_message_rcd info, out string error);

        #endregion

        #endregion
    }
}
