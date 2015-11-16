using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Ipatient_info_Bll
    {
        #region 患者病种
        List<patient_disease> SearchPatientDiseaseList(int hospital_id, int drid, string name, string gender, string cardtype, string cardno, DateTime? createtime1, DateTime? createtime2,
           string alllergic_his, out string error);
        #endregion

        #region 患者地址
        patient_address GetDefaultPatientAddressByPatientId(int patientid);

        #endregion
    }
}
