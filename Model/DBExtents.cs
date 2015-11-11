using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using AutoMapper;
using System.Linq.Expressions;
namespace Model
{
    public static class DBExtents
    {
        #region initial entity
        public static void Initial<T>(this T vobj)
        {

        }
        #endregion
    }
}
