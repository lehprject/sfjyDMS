using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class RoleAuthorization
    {
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }

        public int ParentId { get; set; }

        public string Url { get; set; }

        public string ReferenceUrl { get; set; }

        public string ReferenceUrlEx { get { return string.IsNullOrEmpty(ReferenceUrl) ? Url : ReferenceUrl; } }


        public int Level { get; set; }

        public int Order { get; set; }

    }
}
