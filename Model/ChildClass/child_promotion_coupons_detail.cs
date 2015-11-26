using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class promotion_coupons_detail
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patient_name { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctor_name { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public decimal valuess { get; set; }
    }
}
