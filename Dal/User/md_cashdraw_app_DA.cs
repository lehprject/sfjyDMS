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
    public class md_cashdraw_app_DA : DataAccessBase
    {
        #region 查询

        public List<md_cashdraw_app> SearchCashdrawList(int drid, DateTime? app_time1, DateTime? app_time2, string opuser, DateTime? optime1, DateTime? optime2, int opstatus, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize,out int record, out string error)
        {
            error = string.Empty;
            record = 0;
            try
            {
                #region Command
                //string selectSql = string.Format("select cash.drid,app_time,drawmoney,in_bank,opuser,optime,opstatus,opremark,cash.pkid as pkid from md_cashdraw_app cash WHERE TRUE ");

                //string selectSql = string.Format("select cash.drid,app_time,drawmoney,in_bank,opuser,optime,opstatus,opremark,cash.pkid as pkid,md_docter.name as doctor_name from md_cashdraw_app cash left join md_docter on cash.drid=md_docter.pkid WHERE TRUE ");
                string selectSql = string.Format("select cash.drid,app_time,drawmoney,in_bank,opuser,optime,opstatus,opremark,cash.pkid as pkid,md_hospital.name as hospital,md_docter.name as doctor_name, md_bank.bank as bank from md_cashdraw_app cash left join md_bank on cash.in_bank = md_bank.pkid left join md_docter on cash.drid=md_docter.pkid left join md_hospital on md_docter.hispital_id=md_hospital.pkid WHERE TRUE ");
                string countSql = string.Format("select cash.drid,app_time,drawmoney,in_bank,opuser,optime,opstatus,opremark,cash.pkid as pkid,md_hospital.name as hospital,md_docter.name as doctor_name, md_bank.bank as bank from md_cashdraw_app cash left join md_bank on cash.in_bank = md_bank.pkid left join md_docter on cash.drid=md_docter.pkid left join md_hospital on md_docter.hispital_id=md_hospital.pkid WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                List<md_cashdraw_app> resultList = new List<md_cashdraw_app>();
                #endregion

                #region 搜索条件
                if (drid > 0)
                {
                    conditionSb.Append("AND cash.drid = @drid");
                    paraList.Add(new MySqlParameter("drid", drid));
                }

                if (!string.IsNullOrEmpty(opuser))
                {
                    conditionSb.Append(" AND opuser LIKE CONCAT('%', @opuser, '%') ");
                    paraList.Add(new MySqlParameter("opuser", opuser));
                }

                conditionSb.Append(" AND opstatus = @opstatus ");
                paraList.Add(new MySqlParameter("opstatus", opstatus));

                if (app_time1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(app_time,@app_time1) >= 0 ");
                    paraList.Add(new MySqlParameter("app_time1", app_time1.Value));
                }

                if (app_time2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(app_time,@app_time2) <= 0 ");
                    paraList.Add(new MySqlParameter("app_time2", app_time2.Value));
                }

                if (optime1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(optime,@optime1) >= 0 ");
                    paraList.Add(new MySqlParameter("optime1", optime1.Value));
                }

                if (optime2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(optime,@optime2) <= 0 ");
                    paraList.Add(new MySqlParameter("optime2", optime2.Value));
                }

                #endregion

                #region 排序 分页
                string orderbyStr = string.Empty;
                if (!string.IsNullOrEmpty(orderbyCol) && orderby.HasValue)
                    orderbyStr = orderbyFormat.getSortStr(orderby.Value, orderbyCol);
                else
                    orderbyStr = " order by cash.pkid ";

                selectSql += conditionSb.ToString() + orderbyStr;

                countSql += conditionSb.ToString();
                countSql = string.Format("SELECT COUNT(*) FROM ({0}) AS t", countSql);

                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }
                #endregion

                #region 执行    

                //共多少行
                record = sqlHelper.ExecuteScalar<int>(countSql, paraList.ToArray());
                if (record == 0)
                    return resultList;

                resultList = sqlHelper.ExecuteObjects<md_cashdraw_app>(selectSql, paraList.ToArray()).ToList();
                return resultList;
                #endregion
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        public List<md_cashdraw_app> GetCashdrawByIds(List<int> ids)
        {
            string id = string.Join(",", ids.Select(t => t));
            string sql = @"SELECT *                       
                            FROM md_cashdraw_app                             
                            where FIND_IN_SET(pkid,@ids)";
            List<md_cashdraw_app> resultlist = sqlHelper.ExecuteObjects<md_cashdraw_app>(sql, new MySqlParameter("ids", id));
            return resultlist;
        }

        #endregion

        #region 添加
        public md_cashdraw_app CreateCashdrawApp(md_cashdraw_app info, out string error)
        {
            error = string.Empty;
            try
            {
                db.md_cashdraw_app.Add(info);
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

        #region 修改

        public md_cashdraw_app UpdateCashdraw(md_cashdraw_app info, out string error)
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

        #endregion
    }
}
