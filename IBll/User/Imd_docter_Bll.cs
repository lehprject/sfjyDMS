using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Model;

namespace IBll
{
    public interface Imd_docter_Bll
    {
        #region md_docter
        #region 查询

        md_docter GetDocterById(int id);
        md_docter LoginUtil(string loginname, string loginpass, string keyvalue, out string error);

        #endregion

        #region 修改

        md_docter UpdateDocter(md_docter info, out string error);

        #endregion
        #endregion
    }
}
