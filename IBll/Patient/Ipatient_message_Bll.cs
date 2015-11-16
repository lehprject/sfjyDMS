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


        List<patient_message> SearchMessgaeList(
             int patientid, int drid, int status, string contents,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);

        #endregion

        #endregion
    }
}
