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
    
    public partial class patient_message_rcd
    {
        public int pkid { get; set; }
        public int msg_id { get; set; }
        public string rcd_contents { get; set; }
        public string attach_file1 { get; set; }
        public string attach_file2 { get; set; }
        public System.DateTime createtime { get; set; }
        public string createuser { get; set; }
    }
}