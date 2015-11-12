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
        public List<promotion_events> GetPromotionEventList(int face_type, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            return new promotion_events_DA().GetPromotionEventList(face_type,orderby,orderbyCol,pageIndex,pageSize,out error);
        }
        #endregion
    }
}
