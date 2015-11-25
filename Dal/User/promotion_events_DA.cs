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
        #region 活动相关
        #region 查询
        public List<promotion_events> GetPromotionEventList(int hospital_id, int face_type, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
             DateTime? createtime1, DateTime? createtime2, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            error = string.Empty;
            record = 0;
            try
            {
                string selectSql = "select * from promotion_events where true";
                string countSql = "select * from promotion_events where true";
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


                countSql += conditionSb.ToString();
                countSql = string.Format("SELECT COUNT(*) FROM ({0}) AS t", countSql);

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }

                #endregion

                var resultList = new List<promotion_events>();

                //共多少行
                record = sqlHelper.ExecuteScalar<int>(countSql, paraList.ToArray());
                if (record == 0)
                    return resultList;
                resultList = sqlHelper.ExecuteObjects<promotion_events>(selectSql, paraList.ToArray()).ToList();
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        #endregion

        #region 添加

        public promotion_events CreatePromotion(promotion_events info, out string error)
        {
            error = string.Empty;
            try
            {
                db.promotion_events.Add(info);
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

        #region 优惠券相关

        public List<promotion_coupons> GetPromotionCouponsList(string name, decimal value, int issue_status, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            error = string.Empty;
            record = 0;
            try
            {
                string selectSql = "select * from promotion_coupons where true";
                string countSql = "select * from promotion_coupons where true";
                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();

                #region 条件
                if (!string.IsNullOrEmpty(name))
                {
                    conditionSb.Append(" AND name LIKE CONCAT('%', @name, '%') ");
                    paraList.Add(new MySqlParameter("name", name));
                }

                if (value > 0)
                {
                    conditionSb.Append(" AND value = @value ");
                    paraList.Add(new MySqlParameter("value", value));
                }

                conditionSb.Append(" AND issue_status = @issue_status ");
                paraList.Add(new MySqlParameter("issue_status", issue_status));

                if (!string.IsNullOrEmpty(name))
                {
                    conditionSb.Append(" AND name LIKE CONCAT('%', @name, '%') ");
                    paraList.Add(new MySqlParameter("name", name));
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

                #endregion

                #region 排序 分页
                string orderbyStr = string.Empty;
                if (!string.IsNullOrEmpty(orderbyCol) && orderby.HasValue)
                    orderbyStr = orderbyFormat.getSortStr(orderby.Value, orderbyCol);
                else
                    orderbyStr = " order by pkid desc";

                countSql += conditionSb.ToString();
                countSql = string.Format("SELECT COUNT(*) FROM ({0}) AS t", countSql);

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }

                #endregion

                var resultList = new List<promotion_coupons>();

                //共多少行
                record = sqlHelper.ExecuteScalar<int>(countSql, paraList.ToArray());
                if (record == 0)
                    return resultList;

                resultList = sqlHelper.ExecuteObjects<promotion_coupons>(selectSql, paraList.ToArray()).ToList();
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        #endregion

        #region 优惠卷明细相关

        public List<promotion_coupons_detail> SearchPromotionCouponsList(int coupons_id, int use_status, int business_type, int userid, int drid, DateTime? sendtime1, DateTime? sendtime2, DateTime? usetime1, DateTime? usetime2,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            error = string.Empty;
            record = 0;
            try
            {
                string selectSql = "select * from promotion_coupons where true";
                string countSql = "select * from promotion_coupons where true";
                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();

                #region 条件
                if (coupons_id > 0)
                {
                    conditionSb.Append(" AND coupons_id = @coupons_id ");
                    paraList.Add(new MySqlParameter("coupons_id", coupons_id));
                }

                conditionSb.Append(" AND use_status = @use_status ");
                paraList.Add(new MySqlParameter("use_status", use_status));

                conditionSb.Append(" AND business_type = @business_type ");
                paraList.Add(new MySqlParameter("business_type", business_type));

                if (userid > 0)
                {
                    conditionSb.Append(" AND userid = @userid ");
                    paraList.Add(new MySqlParameter("userid", userid));
                }

                if (drid > 0)
                {
                    conditionSb.Append(" AND drid = @drid ");
                    paraList.Add(new MySqlParameter("drid", drid));
                }

                if (sendtime1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(sendtime,@sendtime1) >= 0 ");
                    paraList.Add(new MySqlParameter("sendtime1", sendtime1.Value));
                }

                if (sendtime2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(sendtime,@sendtime2) <= 0 ");
                    paraList.Add(new MySqlParameter("sendtime2", sendtime2.Value));
                }

                if (usetime1.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(usetime,@usetime1) >= 0 ");
                    paraList.Add(new MySqlParameter("usetime1", usetime1.Value));
                }

                if (usetime2.HasValue)
                {
                    conditionSb.Append(" AND DATEDIFF(usetime,@usetime2) <= 0 ");
                    paraList.Add(new MySqlParameter("usetime2", usetime2.Value));
                }

                #endregion

                #region 排序 分页
                string orderbyStr = string.Empty;
                if (!string.IsNullOrEmpty(orderbyCol) && orderby.HasValue)
                    orderbyStr = orderbyFormat.getSortStr(orderby.Value, orderbyCol);
                else
                    orderbyStr = " order by pkid desc";

                countSql += conditionSb.ToString();
                countSql = string.Format("SELECT COUNT(*) FROM ({0}) AS t", countSql);

                selectSql += conditionSb.ToString() + orderbyStr;
                if (pageIndex > 0)
                {
                    selectSql += " LIMIT @pageIndex , @pageSize ";
                    paraList.Add(new MySqlParameter("pageIndex", (pageIndex - 1) * pageSize));
                    paraList.Add(new MySqlParameter("pageSize", pageSize));
                }

                #endregion

                var resultList = new List<promotion_coupons_detail>();

                //共多少行
                record = sqlHelper.ExecuteScalar<int>(countSql, paraList.ToArray());
                if (record == 0)
                    return resultList;

                resultList = sqlHelper.ExecuteObjects<promotion_coupons_detail>(selectSql, paraList.ToArray()).ToList();
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
