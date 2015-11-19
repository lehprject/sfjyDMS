using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;
using MySql.Data.MySqlClient;
using Share;

namespace Dal
{
    public class patient_order_DA : DataAccessBase
    {
        #region 查询
        public List<patient_order> SearchPatientOrderList(int patient_id, int drid, int service_type, DateTime? createtime1, DateTime? createtime2,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select *,patient_info.name as patient_name from patient_order left join patient_info on patient_order.patient_id = patient_info.pkid WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (patient_id > 0)
                {
                    conditionSb.Append("AND patient_id = @patient_id");
                    paraList.Add(new MySqlParameter("patient_id", patient_id));
                }

                if (drid > 0)
                {
                    conditionSb.Append("AND drid = @drid");
                    paraList.Add(new MySqlParameter("drid", drid));
                }

                if (service_type > 0)
                {
                    conditionSb.Append("AND service_type = @service_type");
                    paraList.Add(new MySqlParameter("service_type", service_type));
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
                    orderbyStr = " order by patient_order.pkid ";

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }
                #endregion

                #region 执行

                var resultList = db.Database.SqlQuery<patient_order>(selectSql, paraList.ToArray()).ToList();
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
    }
}
