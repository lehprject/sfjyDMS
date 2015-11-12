using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Model;
using Model.ConfigClass;
using Share;

namespace Dal
{
    public class md_somebase_info_DA : DataAccessBase
    {
        public List<md_hospital> GetAllHospitalList()
        {
            return db.md_hospital.ToList();
        }
    }
}
