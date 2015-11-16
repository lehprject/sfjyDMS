using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class chk_info
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patient_name { get; set; }

        /// <summary>
        /// 检查项目
        /// </summary>
        public string chk_name { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime chk_time { get; set; }
    }
}
