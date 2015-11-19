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
    public class promotion_events_DA : DataAccessBase
    {
        #region 查询
        public List<promotion_events> GetPromotionEventList(int hospital_id, int face_type, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
             DateTime? createtime1, DateTime? createtime2, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                string selectSql = "select * from promotion_events where true";
                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();

                #region 条件
                if (face_type > 0)
                {
                    conditionSb.Append(" AND face_type = @face_type ");
                    paraList.Add(new MySqlParameter("face_type", face_type));
                }

                if (startdate1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(startdate,@startdate1) >= 0 ");
                    paraList.Add(new MySqlParameter("startdate1", startdate1.Value));
                }

                if (startdate2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(startdate,@startdate2) <= 0 ");
                    paraList.Add(new MySqlParameter("startdate2", startdate2.Value));
                }

                if (enddate1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(enddate,@enddate1) >= 0 ");
                    paraList.Add(new MySqlParameter("enddate1", enddate1.Value));
                }

                if (enddate2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(enddate,@enddate2) <= 0 ");
                    paraList.Add(new MySqlParameter("enddate2", enddate2.Value));
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
                    orderbyStr = " order by pkid desc";

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }

                #endregion

                var resultList = sqlHelper.ExecuteObjects<promotion_events>(selectSql, paraList.ToArray()).ToList();
                return resultList;
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
