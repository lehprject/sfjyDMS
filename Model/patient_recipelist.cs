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
    
    public partial class patient_recipelist
    {
        public int pkid { get; set; }
        public int medical_rcd_id { get; set; }
        public int hospital_id { get; set; }
        public int patient_id { get; set; }
        public string cust_name { get; set; }
        public System.DateTime issue_time { get; set; }
        public System.DateTime createtime { get; set; }
        public string check_result { get; set; }
        public int fileid { get; set; }
        public int drid { get; set; }
    }
}
