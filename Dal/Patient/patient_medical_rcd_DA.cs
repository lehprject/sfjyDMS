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
    public class patient_medical_rcd_DA : DataAccessBase
    { 
        #region  出诊相关 patient_medical_rcd

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


        public List<patient_medical_rcd> SearchMedicalList(string code, int hospital_id, int drid, int patient_id, string cust_name,
            string rcd_result, DateTime? rcd_time1, DateTime? rcd_time2, DateTime? createtime1, DateTime? createtime2, 
            int fileid , string next_chk_item , string next_visit_item, 
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from patient_medical_rcd WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (!string.IsNullOrEmpty(code))
                {
                    conditionSb.Append(" AND code = @code ");
                    paraList.Add(new MySqlParameter("code", code));
                }

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

                if (!string.IsNullOrEmpty(rcd_result))
                {
                    conditionSb.Append(" AND rcd_result = @rcd_result ");
                    paraList.Add(new MySqlParameter("rcd_result", rcd_result));
                }

                if (rcd_time1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(rcd_time,@rcd_time1) >= 0 ");
                    paraList.Add(new MySqlParameter("rcd_time1", rcd_time1.Value));
                }

                if (rcd_time2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(rcd_time,@rcd_time2) <= 0 ");
                    paraList.Add(new MySqlParameter("rcd_time2", rcd_time2.Value));
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

                if (!string.IsNullOrEmpty(next_chk_item))
                {
                    conditionSb.Append(" AND next_chk_item = @next_chk_item ");
                    paraList.Add(new MySqlParameter("next_chk_item", next_chk_item));
                }

                if (!string.IsNullOrEmpty(next_visit_item))
                {
                    conditionSb.Append(" AND next_visit_item = @next_visit_item ");
                    paraList.Add(new MySqlParameter("next_visit_item", next_visit_item));
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

                var resultList = db.Database.SqlQuery<patient_medical_rcd>(selectSql, paraList.ToArray()).ToList();
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
    }
}
