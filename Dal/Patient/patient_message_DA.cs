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
    public class patient_message_DA:DataAccessBase
    {
        #region  患者留言 patient_message

        #region 添加
        public patient_message CreateMessage(patient_message info, out string error)
        {
            error = string.Empty;
            try
            {
                db.patient_message.Add(info);
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


        public List<patient_message> SearchMessgaeList(int hispital_id,
             int patientid, int drid, int status, string contents, DateTime? createtime1, DateTime? createtime2,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select p.createtime,d.name as doctor_name,i.name as patient_name,p.status,contents from patient_message p left join patient_info i on p.patientid =i.pkid left join md_docter d on p.drid=d.pkid left join md_hospital h on d.hispital_id=h.pkid WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件

                if (hispital_id > 0)
                {
                    conditionSb.Append("AND h.pkid = @hispital_id");
                    paraList.Add(new MySqlParameter("hispital_id", drid));
                }

                if (!string.IsNullOrEmpty(contents))
                {
                    conditionSb.Append(" AND p.contents like CONCAT('%', @contents  , '%') ");
                    paraList.Add(new MySqlParameter("contents", contents));
                }

            
                if (drid > 0)
                {
                    conditionSb.Append("AND p.drid = @drid");
                    paraList.Add(new MySqlParameter("drid", drid));
                }
                if (patientid > 0)
                {
                    conditionSb.Append("AND p.patientid = @patientid");
                    paraList.Add(new MySqlParameter("patientid", patientid));
                }

                if (status > 0)
                {
                    conditionSb.Append("AND p.status = @status");
                    paraList.Add(new MySqlParameter("status", status));
                }

                if (createtime1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(p.createtime,@createtime1) >= 0 ");
                    paraList.Add(new MySqlParameter("createtime1", createtime1.Value));
                }

                if (createtime2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(p.createtime,@createtime2) <= 0 ");
                    paraList.Add(new MySqlParameter("createtime2", createtime2.Value));
                }

                #endregion

                #region 排序 分页
                string orderbyStr = string.Empty;
                if (!string.IsNullOrEmpty(orderbyCol) && orderby.HasValue)
                    orderbyStr = orderbyFormat.getSortStr(orderby.Value, orderbyCol);
                else
                    orderbyStr = " order by p.pkid ";

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }
                #endregion

                #region 执行

                var resultList = db.Database.SqlQuery<patient_message>(selectSql, paraList.ToArray()).ToList();
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

        #region  患者留言记录回复  patient_message_rcd
        #region 添加
        public patient_message_rcd CreateMessageRcd(patient_message_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                db.patient_message_rcd.Add(info);
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

        #endregion
    }
}
