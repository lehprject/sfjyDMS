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
    public class chk_info_DA:DataAccessBase
    {
        #region 检查单 chk_info 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        public List<chk_info> SearchChkInfoList(
             int medical_id ,   int patient_id ,   int chk_type_id ,   int chk_demo_id ,   int fileid ,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from chk_info WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (medical_id > 0)
                {
                    conditionSb.Append(" AND medical_id = @medical_id ");
                    paraList.Add(new MySqlParameter("medical_id", medical_id));
                }

                if (patient_id > 0)
                {
                    conditionSb.Append(" AND patient_id = @patient_id ");
                    paraList.Add(new MySqlParameter("patient_id", patient_id));
                }

                if (chk_type_id > 0)
                {
                    conditionSb.Append(" AND chk_type_id = @chk_type_id ");
                    paraList.Add(new MySqlParameter("chk_type_id", chk_type_id));
                }

                if (chk_demo_id > 0)
                {
                    conditionSb.Append(" AND chk_demo_id = @chk_demo_id ");
                    paraList.Add(new MySqlParameter("chk_demo_id", chk_demo_id));
                }

                if (fileid > 0)
                {
                    conditionSb.Append(" AND fileid = @fileid ");
                    paraList.Add(new MySqlParameter("fileid", fileid));
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

                var resultList = db.Database.SqlQuery<chk_info>(selectSql, paraList.ToArray()).ToList();
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

        #region 检查单类型 chk_type 相关

        #region 添加
        
        #endregion

        #region 修改

        #endregion

        #region 查询
        public List<chk_type> SearchChkTypeList(
             string name,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from chk_type WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件

                if (!string.IsNullOrEmpty(name))
                {
                    conditionSb.Append(" AND name like CONCAT('%'@,name,'%') ");
                    paraList.Add(new MySqlParameter("name", name));
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

                var resultList = db.Database.SqlQuery<chk_type>(selectSql, paraList.ToArray()).ToList();
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

        #region 检查单模板 chk_demo 相关

        #region 添加

        #endregion

        #region 修改

        #endregion

        #region 查询
        public List<chk_demo> SearchChkTypeList(
             int typeid, string chk_item,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from chk_demo WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件

                if (typeid > 0)
                {
                    conditionSb.Append(" AND typeid = @typeid ");
                    paraList.Add(new MySqlParameter("typeid", typeid));
                }

                if (!string.IsNullOrEmpty(chk_item))
                {
                    conditionSb.Append(" AND chk_item like CONCAT('%'@,chk_item,'%') ");
                    paraList.Add(new MySqlParameter("chk_item", chk_item));
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

                var resultList = db.Database.SqlQuery<chk_demo>(selectSql, paraList.ToArray()).ToList();
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
