using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBll;
using Dal;
using Model.ConfigClass;
using Share;

namespace Bll
{
    public class promotion_events_Bll
    {
        #region 查询
        /// <summary>
        /// 查询活动信息
        /// </summary>
        /// <param name="face_type"></param>
        /// <param name="orderby"></param>
        /// <param name="orderbyCol"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<promotion_events> GetPromotionEventList(int hospital_id, int face_type, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
             DateTime? createtime1, DateTime? createtime2, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            return new promotion_events_DA().GetPromotionEventList(hospital_id, face_type, startdate1, startdate2, enddate1, enddate2,
             createtime1, createtime2,orderby,orderbyCol,pageIndex,pageSize,out error);
        }
        #endregion
    }
}
