using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Imd_account_Bll
    {
        #region 添加
        md_dr_account CreateAccount(md_dr_account info, out string error);

        bool CreateAccountList(List<md_dr_account> list, out string error);
        #endregion
    }
}
