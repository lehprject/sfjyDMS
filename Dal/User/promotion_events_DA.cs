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
        public List<promotion_events> GetPromotionEventList(int face_type, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                string selectSql = "select * from promotion_events where true";
                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();

                if (face_type > 0)
                {
                    conditionSb.Append(" AND face_type = @face_type ");
                    paraList.Add(new MySqlParameter("face_type", face_type));
                }

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
