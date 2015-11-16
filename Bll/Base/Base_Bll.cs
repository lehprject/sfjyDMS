using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBll;
using Dal;
using System.Linq.Expressions;

namespace Bll
{
    public  class Base_Bll
    {
        #region fields
        base_DA baseDa = new base_DA();
        #endregion

        #region 获取对象 或对象列表
        public T GetInfo<T>(int pkid)
        {
            return baseDa.GetInfo<T>(pkid);
        }

        public T GetInfo<T>(int pkid, string columnName)
        {
            return baseDa.GetInfo<T>(pkid, columnName);
        }

        public T GetInfo<T>(int pkid, System.Linq.Expressions.Expression<Func<T, object>> selector )
        {
            return baseDa.GetInfo<T>(pkid, selector);
        }
        #endregion

        #region 关联查询

        /// <summary>
        /// 获取对象关联其它表的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="selector">如 t => new { t.memberno,t.provincename }</param>
        public void GetFor<T>(T info, Expression<Func<T, object>> selector)
        {
            try
            {
                baseDa.GetFor<T>(info, selector);
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 获取列表的关联其它表的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="selector">如 t => new { t.memberno,t.provincename }</param>
        public void GetFor<T>(IEnumerable<T> list, Expression<Func<T, object>> selector)
        {
            try
            {
                baseDa.GetFor<T>(list, selector);
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
    }
}
