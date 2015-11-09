using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Model
{
    public sealed partial class MySqlHelper
    {
        #region 实例方法

        public T ExecuteObject<T>(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteObject<T>(this.ConnectionString, commandText, parms);
        }

        public T ExecuteObject<T>(System.Data.CommandType commandType,string commandText, params MySqlParameter[] parms)
        {
            return ExecuteObject<T>(this.ConnectionString,commandType, commandText, parms);
        }

        public List<T> ExecuteObjects<T>(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteObjects<T>(this.ConnectionString, commandText, parms);
        }
        public List<T> ExecuteObjects<T>(System.Data.CommandType commandType,string commandText, params MySqlParameter[] parms)
        {
            return ExecuteObjects<T>(this.ConnectionString,commandType, commandText, parms);
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">用于对象,不能用于基本类型:int string等</typeparam>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static T ExecuteObject<T>(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            //DataTable dt = ExecuteDataTable(connectionString, commandText, parms);
            //return AutoMapper.Mapper.DynamicMap<List<T>>(dt.CreateDataReader()).FirstOrDefault();
            using (MySqlDataReader reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader).FirstOrDefault();
            }
        }

        public static T ExecuteObject<T>(string connectionString,System.Data.CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            using (MySqlDataReader reader = ExecuteDataReader(connectionString,commandType, commandText, parms))
            {
                 return AutoMapper.Mapper.DynamicMap<List<T>>(reader).FirstOrDefault();                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">用于对象,不能用于基本类型:int string等</typeparam>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static List<T> ExecuteObjects<T>(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            //DataTable dt = ExecuteDataTable(connectionString, commandText, parms);
            //return AutoMapper.Mapper.DynamicMap<List<T>>(dt.CreateDataReader());
            using (MySqlDataReader reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader);
            }
        }
        public static List<T> ExecuteObjects<T>(string connectionString,System.Data.CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            using (MySqlDataReader reader = ExecuteDataReader(connectionString,commandType, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader);
            }
        }

        #endregion
    }
}
