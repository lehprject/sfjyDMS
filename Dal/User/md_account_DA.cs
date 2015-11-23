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
    public class md_account_DA : DataAccessBase
    {
        #region 添加
        public md_dr_account CreateAccount(md_dr_account info, out string error)
        {
            error = string.Empty;
            try
            {
                db.md_dr_account.Add(info);
                db.SaveChanges();
                return info;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }

        }
        #endregion
    }
}
