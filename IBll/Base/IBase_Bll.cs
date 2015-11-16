using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Linq.Expressions;

namespace IBll
{
    public interface IBase_Bll
    {
        #region 获取对象 或对象列表
        T GetInfo<T>(int pkid);
        T GetInfo<T>(int pkid, string columnName);
        T GetInfo<T>(int pkid, System.Linq.Expressions.Expression<Func<T, object>> selector);
        #endregion

        #region 关联属性
        void GetFor<T>(T info, Expression<Func<T, object>> selector);

        void GetFor<T>(IEnumerable<T> list, Expression<Func<T, object>> selector);
        #endregion
         
    }
}
