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
        public List<promotion_events> GetPromotionEventList(string name, int hospital_id, int face_type, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
             DateTime? createtime1, DateTime? createtime2, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            return new promotion_events_DA().GetPromotionEventList(name,hospital_id, face_type, startdate1, startdate2, enddate1, enddate2,
             createtime1, createtime2, orderby, orderbyCol, pageIndex, pageSize, out record, out error);
        }

        public List<promotion_events> GetAllPromotionEventl()
        {
            return new promotion_events_DA().GetAllPromotionEventl();
        }
        #endregion

        #region 添加
        public promotion_events CreatePromotion(promotion_events info, out string error)
        {
            error = string.Empty;
            try
            {
                promotion_events_DA da = new promotion_events_DA();
                da.CreatePromotion(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error += Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }

        }
        #endregion

        #region 优惠券相关

        #region 添加

        public bool CreatePromotionCoupons(promotion_coupons info, List<promotion_coupons_detail> detailList, List<promotion_coupons_usecase> usecaselist, out string error)
        {
            error = string.Empty;
            try
            {
                promotion_events_DA recipeDa = new promotion_events_DA();
                bool success = recipeDa.CreatePromotionCoupons(info, detailList, usecaselist, out error);
                return success;

            }
            catch (Exception ex)
            {
                error = Share.BaseTool.FormatExceptionMessage(ex);
                return false;
            }


        }

        #endregion


        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="issue_status"></param>
        /// <param name="startdate1"></param>
        /// <param name="startdate2"></param>
        /// <param name="enddate1"></param>
        /// <param name="enddate2"></param>
        /// <param name="orderby"></param>
        /// <param name="orderbyCol"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="record"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<promotion_coupons> GetPromotionCouponsList(string name, decimal value, int issue_status, DateTime? startdate1, DateTime? startdate2, DateTime? enddate1, DateTime? enddate2,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            error = string.Empty;
            record = 0;
            return new promotion_events_DA().GetPromotionCouponsList(name, value, issue_status, startdate1, startdate2, enddate1, enddate2,
             orderby, orderbyCol, pageIndex, pageSize, out record, out error);
        }

        #endregion


        #region 优惠卷明细相关

        public List<promotion_coupons_detail> SearchPromotionCouponsList(int coupons_id, int use_status, int business_type, int userid, int drid, DateTime? sendtime1, DateTime? sendtime2, DateTime? usetime1, DateTime? usetime2,
           orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out int record, out string error)
        {
            error = string.Empty;
            record = 0;
            return new promotion_events_DA().SearchPromotionCouponsList(coupons_id, use_status, business_type, userid, drid, sendtime1, sendtime2, usetime1, usetime2,
             orderby, orderbyCol, pageIndex, pageSize, out record, out error);
        }

        #endregion
    }
}
