using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Model;
using MySql.Data.MySqlClient;

namespace Dal
{
    public class base_DA:DataAccessBase
    { 

        #region 通过主键list 获取对象list

        #region String类型
        public List<T> GetInfoListByList<T>(IEnumerable<string> list, string columnName)
        {
            if (list == null || list.Count() == 0)
                return new List<T>();

            Type type = typeof(T);
            string tableName = type.Name;
            if (type.GetProperty(columnName) == null)
                return null;
            string listStr = string.Join(",", list);
            string sql = string.Format("select * from {0} where FIND_IN_SET( {1} ,@list) ", tableName, columnName);

            #region execute

            return sqlHelper.ExecuteObjects<T>(sql, new MySqlParameter("list", listStr));
             
            #endregion
        }

        public List<T> GetInfoListByList<T>(IEnumerable<string> list)
        {
            return GetInfoListByList<T>(list, "pkid");
        }

        public List<T> GetInfoListByList<T>(IEnumerable<string> list, System.Linq.Expressions.Expression<Func<T, object>> selector)
        {
            string columnName = string.Empty;
            {
                System.Linq.Expressions.UnaryExpression body = selector.Body as System.Linq.Expressions.UnaryExpression;
                if (body != null)
                {
                    if (body.Operand is System.Linq.Expressions.MemberExpression)
                    {
                        columnName = (body.Operand as System.Linq.Expressions.MemberExpression).Member.Name;
                    }
                }

            }
            if (string.IsNullOrEmpty(columnName))
            {
                System.Linq.Expressions.MemberExpression body = selector.Body as System.Linq.Expressions.MemberExpression;
                if (body != null)
                    columnName = body.Member.Name;
            }

            return GetInfoListByList<T>(list, columnName);
        }

        #endregion

        #region Int类型
        public List<T> GetInfoListByList<T>(IEnumerable<int> list, string columnName)
        {
            return GetInfoListByList<T>(list.Select(t => t.ToString()), columnName);
        }

        public List<T> GetInfoListByList<T>(IEnumerable<int> list)
        {
            return GetInfoListByList<T>(list.Select(t => t.ToString()));
        }

        public List<T> GetInfoListByList<T>(IEnumerable<int> list, System.Linq.Expressions.Expression<Func<T, object>> selector)
        {
            return GetInfoListByList<T>(list.Select(t => t.ToString()), selector);
        }
        #endregion

        #endregion

        #region 通过主机获取Info对象
         
        public T GetInfo<T>(int pkid, string columnName)
        {
         
            Type type = typeof(T);
            string tableName = type.Name;
            if (type.GetProperty(columnName) == null)
                return default(T);
            string sql = string.Format("select * from {0} where  {1} == @pkid ", tableName, columnName);

            #region execute

            return sqlHelper.ExecuteObjects<T>(sql,   new MySqlParameter("pkid", pkid)).FirstOrDefault();

            #endregion
        }

        public T GetInfo<T>(int pkid)
        {
            return GetInfo<T>(pkid, "pkid");
        }

        public T GetInfo<T>(int pkid, System.Linq.Expressions.Expression<Func<T, object>> selector)
        {
            string columnName = string.Empty;
            {
                System.Linq.Expressions.UnaryExpression body = selector.Body as System.Linq.Expressions.UnaryExpression;
                if (body != null)
                {
                    if (body.Operand is System.Linq.Expressions.MemberExpression)
                    {
                        columnName = (body.Operand as System.Linq.Expressions.MemberExpression).Member.Name;
                    }
                }

            }
            if (string.IsNullOrEmpty(columnName))
            {
                System.Linq.Expressions.MemberExpression body = selector.Body as System.Linq.Expressions.MemberExpression;
                if (body != null)
                    columnName = body.Member.Name;
            }

            return GetInfo<T>(pkid, columnName);
        }
         
         
        #endregion

    }

   
}
