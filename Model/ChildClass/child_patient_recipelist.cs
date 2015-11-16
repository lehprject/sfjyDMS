using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class patient_recipelist
    {
        public string patient_name { get; set; }

        public string gender { get; set; }

        public DateTime birth { get; set; }
        public int patient_age { get; set; }

        public string alllergic_his { get; set; }

        public string address { get; set; }

        public List<patient_recipelist_druguse> druguselist { get; set; }
    }
}
