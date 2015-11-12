using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Imd_druginfo_Bll
    {
        #region 查询
        List<md_druginfo> SearchDruginfoList(string drugname, string standard, int producerid, string cust_name, string sub_name,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        #endregion
    }
}
