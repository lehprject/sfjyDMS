using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ipromotion_events_Bll
    {
        #region 活动相关
        #region 查询
        List<promotion_events> GetPromotionEventList(int hospital_id, int face_type, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
             DateTime? createtime1, DateTime? createtime2, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error);
        #endregion

        #region 添加
        promotion_events CreatePromotion(promotion_events info, out string error);
        #endregion

        #endregion

        #region 优惠券相关
        List<promotion_coupons> GetPromotionCouponsList(string name, decimal value, int issue_status, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
           orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error);
        #endregion


        #region 优惠卷明细相关

        

        #endregion
    }
}
