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
    
    public partial class md_cashdraw_app
    {
        public int pkid { get; set; }
        public string drid { get; set; }
        public System.DateTime app_time { get; set; }
        public decimal drawmoney { get; set; }
        public int in_bank { get; set; }
        public string opuser { get; set; }
        public System.DateTime optime { get; set; }
        public int opstatus { get; set; }
        public string opremark { get; set; }
    }
}
