using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class promotion_coupons
    {
        /// <summary>
        /// 库存数量
        /// </summary>
        public int stock { get; set; }

        /// <summary>
        /// 所在活动
        /// </summary>
        public string events { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public decimal valuess { get; set; }
    }
}
