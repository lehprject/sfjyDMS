using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Dal
{
    public class md_docter_DA : DataAccessBase
    {
        #region 查询

        public md_docter GetDocterById(int id)
        {
            return db.md_docter.FirstOrDefault(m => m.pkid == id);
        }

        public md_docter LoginUtil(string loginname, string loginpass)
        {
            return db.md_docter.FirstOrDefault(tb => tb.name == loginname && tb.login_pwd == loginpass);
        }

        #endregion

        #region 修改

        public md_docter UpdateDocter(md_docter info,out string error)
        {
            error = string.Empty;
            try
            {
                db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return info;
            }
            catch (Exception ex)
            {
                error += ex.ToString();
                return info;
            }
        }

        #endregion
    }
}
