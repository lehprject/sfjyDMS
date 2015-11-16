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


        public List<patient_message> SearchMessgaeList(
             int patientid,int drid,int status,string contents,
             orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from patient_message WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (!string.IsNullOrEmpty(contents))
                {
                    conditionSb.Append(" AND contents like CONCAT('%', @contents  , '%') ");
                    paraList.Add(new MySqlParameter("contents", contents));
                }

            
                if (drid > 0)
                {
                    conditionSb.Append("AND drid = @drid");
                    paraList.Add(new MySqlParameter("drid", drid));
                }
                if (patientid > 0)
                {
                    conditionSb.Append("AND patientid = @patientid");
                    paraList.Add(new MySqlParameter("patientid", patientid));
                }

                if (status > 0)
                {
                    conditionSb.Append("AND status = @status");
                    paraList.Add(new MySqlParameter("status", status));
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
    }
}
