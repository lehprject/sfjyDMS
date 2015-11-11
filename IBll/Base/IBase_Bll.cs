using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface IBase_Bll
    {
        #region 获取对象 或对象列表
        T GetInfo<T>(int pkid);
        T GetInfo<T>(int pkid, string columnName);
        T GetInfo<T>(int pkid, System.Linq.Expressions.Expression<Func<T, object>> selector);
        #endregion
         
    }
}
