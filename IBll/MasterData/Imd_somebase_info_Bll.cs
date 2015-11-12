using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Imd_somebase_info_Bll
    {
        List<md_hospital> GetAllHospitalList();
    }
}
