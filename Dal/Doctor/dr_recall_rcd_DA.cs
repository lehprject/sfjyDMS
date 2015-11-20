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
    public class dr_recall_rcd_DA:DataAccessBase
    {
        #region 添加
        public dr_recall_rcd CreateDrRecallRcd(dr_recall_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                db.dr_recall_rcd.Add(info);
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
