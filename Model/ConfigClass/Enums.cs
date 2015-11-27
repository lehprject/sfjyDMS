using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ConfigClass
{
    #region 排序相关枚举
    public enum orderbyEnum
    {
        升序 = 1,
        降序 = 2

    }
    public class orderbyFormat
    {

        /// <summary>
        /// 返回排序SQL语句
        /// </summary>
        /// <param name="srt">排序参数</param>
        /// <param name="cols">排序字段</param>
        /// <returns></returns>
        public static string getSortStr(orderbyEnum srt, string cols)
        {
            string result = string.Empty;
            switch (srt)
            {
                case orderbyEnum.降序:
                    result = " order by " + cols + " desc ";
                    break;

                case orderbyEnum.升序:
                    result = " order by " + cols + " asc ";
                    break;

                default:
                    result = " order by " + cols + " desc ";
                    break;

            }
            return result;
        }
    }

    /// <summary>
    /// 巧搭配 搜索排序
    /// </summary>
    public enum PackageSearchEnum
    {
        默认 = 1,
        销量 = 2
    }

    /// <summary>
    /// 会员 搜索排序
    /// </summary>
    public enum MemberSearchEnum
    {
        默认 = 1,
        姓名 = 2,
        购药时间 = 3
    }

    /// <summary>
    /// 提现申请处理状态
    /// </summary>
    public enum CashdrawStatus
    {
        未处理 = 0,
        已处理 = 1
    }


    /// <summary>
    /// 优惠券状态
    /// </summary>
    public enum CouponsStatus
    {
        无效 = 0,
        有效 = 1
    }

    /// <summary>
    /// 大于等于状态
    /// </summary>
    public enum MoreThanStatus
    {
        大于 = 1,
        大于等于 = 2,
        小于 = 3,
        小于等于 = 4,
    }

    /// <summary>
    /// 活动类型
    /// </summary>
    public enum PromotionEventType
    {
        内部活动 = 1,
        外部活动 = 2,
    }

    /// <summary>
    /// 优惠券业务类型
    /// </summary>
    public enum CouponsBusinessType
    {
        在线咨询 = 1,
        付费电话 = 2,
    }
    #endregion
}
