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
        #region 查询
        List<promotion_events> GetPromotionEventList(int face_type, orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        #endregion
    }
}
