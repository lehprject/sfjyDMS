using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBll;
using Dal;
using Model;
using Model.ConfigClass;
using Share;

namespace Bll
{
    public class md_somebase_info_Bll
    {
        public List<md_hospital> GetAllHospitalList()
        {
            return new md_somebase_info_DA().GetAllHospitalList();
        }
    }
}
