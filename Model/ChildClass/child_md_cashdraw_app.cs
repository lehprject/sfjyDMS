using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class md_cashdraw_app
    {
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctor_name { get; set; }

        /// <summary>
        /// 所属医院
        /// </summary>
        public string hospital { get; set; }

        /// <summary>
        /// 转入银行
        /// </summary>
        public string bank { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string apptime { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string _optime { get; set; }
    }
}
