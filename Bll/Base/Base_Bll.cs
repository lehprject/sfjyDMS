using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBll;
using Dal;

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

        #endregion
    }
}
