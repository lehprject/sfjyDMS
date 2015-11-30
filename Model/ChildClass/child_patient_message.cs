using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class patient_message
    {
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctor_name { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patient_name { get; set; }
    }
}
