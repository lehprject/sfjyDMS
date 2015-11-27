using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

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

        public List<md_docter> GetDoctorByIds(List<string> ids)
        {
            string id = string.Join(",", ids.Select(t => t));
            string sql = @"SELECT *                       
                            FROM md_docter                             
                            where FIND_IN_SET(pkid,@ids)";
            List<md_docter> resultlist = sqlHelper.ExecuteObjects<md_docter>(sql, new MySqlParameter("ids", id));
            return resultlist;
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
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        public bool UpdateChashdrawList(List<md_docter> list, out string error)
        {
            error = string.Empty;
            try
            {
                foreach (var item in list)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return false;
            }
        }

        #endregion
    }
}
