using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Dal
{
     public class DataAccessBase
    {
         protected sfjyEntities db = new sfjyEntities();
         protected MySqlHelper sqlHelper = new MySqlHelper();
    }
}
