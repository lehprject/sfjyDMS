//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class promotion_coupons
    {
        public int pkid { get; set; }
        public string name { get; set; }
        public decimal values { get; set; }
        public System.DateTime startdate { get; set; }
        public System.DateTime enddate { get; set; }
        public int issue_num { get; set; }
        public int issue_status { get; set; }
        public int events_id { get; set; }
    }
}
