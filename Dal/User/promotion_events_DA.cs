using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;
using MySql.Data.MySqlClient;

namespace Dal
{
    public class promotion_events_DA : DataAccessBase
    {
        #region 查询
        public List<promotion_events> GetPromotionEventList(int face_type)
        {
            DateTime today = DateTime.Now;
            string sql = "select * from promotion_events where face_type=@face_type and enddate=@enddate";

            List<MySqlParameter> parms = new List<MySqlParameter>();
            parms.Add(new MySqlParameter("face_type", face_type));
            parms.Add(new MySqlParameter("enddate", today));
            var parmsArr = parms.ToArray();

            List<promotion_events> resultlist = sqlHelper.ExecuteObjects<promotion_events>(sql, parmsArr);
            return resultlist;
        }



        #endregion
    }
}
