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
    public class dr_visit_DA : DataAccessBase
    {
        #region  出诊相关 dr_visit

        #region 添加
        public dr_visit CreateVisit(dr_visit info, out string error)
        {
            error = string.Empty;
            try
            {
                db.dr_visit.Add(info);
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

        #endregion

        #region 查询

        /// <summary>
        /// 对出诊表各个字段查询
        /// </summary>
        /// <param name="hospital_id"></param>
        /// <param name="drid"></param>
        /// <param name="visit_date1"></param>
        /// <param name="visit_date2"></param>
        /// <param name="visit_time"></param>
        /// <param name="service_type"></param>
        /// <param name="status"></param>
        /// <param name="createtime1"></param>
        /// <param name="createtime2"></param>
        /// <param name="orderby">顺序 降序</param>
        /// <param name="orderbyCol">排序字段,传入dr_visit的字段,不需要前缀</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="error"></param>
        /// <returns>null则出错</returns>
        public List<dr_visit> SearchVisitList(int hospital_id, int drid, DateTime? visit_date1, DateTime? visit_date2, string visit_time,
            int service_type, int status, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from dr_visit WHERE TRUE ");
                 
                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (hospital_id > 0)
                {
                    conditionSb.Append("AND hospital_id = @hospital_id");
                    paraList.Add(new MySqlParameter("hospital_id", hospital_id));
                }

                if (drid > 0)
                {
                    conditionSb.Append("AND drid = @drid");
                    paraList.Add(new MySqlParameter("drid", drid));
                }
                 
                if (visit_date1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(visit_date,@visit_date1) >= 0 ");
                    paraList.Add(new MySqlParameter("visit_date1", visit_date1.Value));
                }

                if (visit_date2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(visit_date,@visit_date2) <= 0 ");
                    paraList.Add(new MySqlParameter("visit_date2", visit_date2.Value));
                }

                if (!string.IsNullOrEmpty(visit_time))
                {
                    conditionSb.Append(" AND visit_time = @visit_time ");
                    paraList.Add(new MySqlParameter("visit_time", visit_time));
                }

                if(service_type > 0)
                {
                    conditionSb.Append("AND service_type = @service_type");
                    paraList.Add(new MySqlParameter("service_type", service_type));
                }

                if (status > 0)
                {
                    conditionSb.Append("AND status = @status");
                    paraList.Add(new MySqlParameter("status", status));
                }

                if (createtime1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(createtime,@createtime1) >= 0 ");
                    paraList.Add(new MySqlParameter("createtime1", createtime1.Value));
                }

                if (createtime2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(createtime,@createtime2) <= 0 ");
                    paraList.Add(new MySqlParameter("createtime2", createtime2.Value));
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

                var resultList = db.Database.SqlQuery<dr_visit>(selectSql, paraList.ToArray()).ToList();
                return resultList;
                #endregion
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        #endregion
         
        #endregion

        #region  预约相关 dr_pre_visit

        #region 添加
        public dr_pre_visit CreatePreVisit(dr_pre_visit info, out string error)
        {
            error = string.Empty;
            try
            {
                db.dr_pre_visit.Add(info);
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

        #endregion

        #region 查询

         
        public List<dr_pre_visit> SearchPreVisitList( int visit_id, int patient_id, DateTime? pre_date1, DateTime? pre_date2,
            string pre_time, int pre_type, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from dr_pre_visit WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件  
                if (visit_id > 0)
                {
                    conditionSb.Append(" AND visit_id = @visit_id ");
                    paraList.Add(new MySqlParameter("visit_id", visit_id));
                }

                if (patient_id > 0)
                {
                    conditionSb.Append(" AND patient_id = @patient_id ");
                    paraList.Add(new MySqlParameter("patient_id", patient_id));
                }

                if (pre_date1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(pre_date,@pre_date1) >= 0 ");
                    paraList.Add(new MySqlParameter("pre_date1", pre_date1.Value));
                }

                if (pre_date2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(pre_date,@pre_date2) <= 0 ");
                    paraList.Add(new MySqlParameter("pre_date2", pre_date2.Value));
                }


                if (!string.IsNullOrEmpty(pre_time))
                {
                    conditionSb.Append(" AND pre_time = @pre_time ");
                    paraList.Add(new MySqlParameter("pre_time", pre_time));
                }

                if (pre_type > 0)
                {
                    conditionSb.Append(" AND pre_type = @pre_type ");
                    paraList.Add(new MySqlParameter("pre_type", pre_type));
                }

                if (createtime1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(createtime,@createtime1) >= 0 ");
                    paraList.Add(new MySqlParameter("createtime1", createtime1.Value));
                }

                if (createtime2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(createtime,@createtime2) <= 0 ");
                    paraList.Add(new MySqlParameter("createtime2", createtime2.Value));
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

                var resultList = db.Database.SqlQuery<dr_pre_visit>(selectSql, paraList.ToArray()).ToList();
                return resultList;
                #endregion
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        public List<dr_pre_visit> GetPreVisitListByVisitID(int visitID)
        {
            return db.dr_pre_visit.Where(t => t.visit_id == visitID).ToList();
        }

        #endregion

        #endregion
    }
}
