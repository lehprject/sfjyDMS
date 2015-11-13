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
    public class md_druginfo_DA : DataAccessBase
    {
        public List<md_druginfo> SearchDruginfoList(string drugname, string standard, int producerid, string cust_name, string sub_name,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from md_druginfo WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (!string.IsNullOrEmpty(drugname))
                {
                    conditionSb.Append(" AND drugname LIKE CONCAT('%', @drugname, '%') ");
                    paraList.Add(new MySqlParameter("drugname", drugname));
                }

                if (!string.IsNullOrEmpty(standard))
                {
                    conditionSb.Append(" AND standard LIKE CONCAT('%', @standard, '%') ");
                    paraList.Add(new MySqlParameter("standard", standard));
                }

                if (producerid > 0)
                {
                    conditionSb.Append("AND producerid = @producerid");
                    paraList.Add(new MySqlParameter("producerid", producerid));
                }

                if (!string.IsNullOrEmpty(cust_name))
                {
                    conditionSb.Append(" AND cust_name LIKE CONCAT('%', @cust_name, '%') ");
                    paraList.Add(new MySqlParameter("cust_name", cust_name));
                }

                if (!string.IsNullOrEmpty(sub_name))
                {
                    conditionSb.Append(" AND sub_name LIKE CONCAT('%', @sub_name, '%') ");
                    paraList.Add(new MySqlParameter("sub_name", sub_name));
                }

                #endregion

                #region 排序 分页
                string orderbyStr = string.Empty;
                if (!string.IsNullOrEmpty(orderbyCol) && orderby.HasValue)
                    orderbyStr = orderbyFormat.getSortStr(orderby.Value, orderbyCol);
                else
                    orderbyStr = " order by pkid ";

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }
                #endregion

                #region 执行

                var resultList = db.Database.SqlQuery<md_druginfo>(selectSql, paraList.ToArray()).ToList();
                return resultList;
                #endregion
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }
    }
}
