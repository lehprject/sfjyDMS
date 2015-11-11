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
    public class patient_recipelist_DA : DataAccessBase
    {
        #region  处方相关 patient_recipelist

        #region 添加
        public bool CreateReciptList(patient_medical_rcd info,List<patient_recipelist_druguse> drugList, out string error)
        {
            error = string.Empty;
            try
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    #region 处方

                    if (info.pkid == 0)
                    {
                        db.patient_medical_rcd.Add(info);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                    }
                    #endregion

                    #region 用药
                    if (drugList != null)
                    {
                        foreach(var item in drugList)
                        {
                            if(item.pkid == 0)
                            {
                                item.recipelist_id = info.pkid;
                                db.patient_recipelist_druguse.Add(item);
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                    }
                    #endregion

                    tran.Commit();
                    return true;
                }

                
                
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return false;
            }

        }
        #endregion

        #region 修改

        #endregion

        #region 查询


        public List<patient_recipelist> SearchVisitList(int medical_rcd_id ,int hospital_id ,int patient_id ,string cust_name ,
            DateTime? issue_time1 ,DateTime? issue_time2 , DateTime? createtime1 ,DateTime? createtime2,string check_result ,int fileid, 
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from patient_recipelist WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件

                if (medical_rcd_id > 0)
                {
                    conditionSb.Append("AND medical_rcd_id = @medical_rcd_id");
                    paraList.Add(new MySqlParameter("medical_rcd_id", medical_rcd_id));
                }

                if (hospital_id > 0)
                {
                    conditionSb.Append("AND hospital_id = @hospital_id");
                    paraList.Add(new MySqlParameter("hospital_id", hospital_id));
                }

                if (patient_id > 0)
                {
                    conditionSb.Append("AND patient_id = @patient_id");
                    paraList.Add(new MySqlParameter("patient_id", patient_id));
                }

                if (!string.IsNullOrEmpty(cust_name))
                {
                    conditionSb.Append(" AND cust_name = @cust_name ");
                    paraList.Add(new MySqlParameter("cust_name", cust_name));
                }


                if (issue_time1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(issue_time,@issue_time1) >= 0 ");
                    paraList.Add(new MySqlParameter("issue_time1", issue_time1.Value));
                }

                if (issue_time2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(issue_time,@issue_time2) <= 0 ");
                    paraList.Add(new MySqlParameter("issue_time2", issue_time2.Value));
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

                if (fileid > 0)
                {
                    conditionSb.Append("AND fileid = @fileid");
                    paraList.Add(new MySqlParameter("fileid", fileid));
                }

                if (!string.IsNullOrEmpty(check_result))
                {
                    conditionSb.Append(" AND check_result = @check_result ");
                    paraList.Add(new MySqlParameter("check_result", check_result));
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

                var resultList = db.Database.SqlQuery<patient_recipelist>(selectSql, paraList.ToArray()).ToList();
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

        #region  处方用药相关 patient_recipelist_druguse

        #region 添加
        public patient_medical_rcd Add(patient_medical_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                db.patient_medical_rcd.Add(info);
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
         
        public List<patient_recipelist_druguse> GetRecipeDrugListByReciptID(int reciptID)
        {
            return db.patient_recipelist_druguse.Where(t => t.recipelist_id == reciptID).ToList();
        }

        #endregion

        #endregion
    }
}
