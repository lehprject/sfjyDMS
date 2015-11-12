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
        List<patient_disease> SearchPatientDiseaseList(string name, string mobile, string gender, string cardtype, string cardno, DateTime? createtime,
           string alllergic_his, out string error);
        #endregion
    }
}
