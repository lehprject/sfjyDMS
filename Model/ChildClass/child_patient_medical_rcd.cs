using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class patient_medical_rcd
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patient_name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }

        public DateTime birth { get; set; }

        public int patient_age { get; set; }

        public string address { get; set; }

        public string hospital_name { get; set; }

        public string doctor_name { get; set; }
    }
}
