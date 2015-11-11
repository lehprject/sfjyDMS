using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Dal
{
    public class promotion_events_DA : DataAccessBase
    {
        #region 查询
        public List<promotion_events> GetPromotionEventList(int face_type)
        {
            return db.promotion_events.Where(m=>m.face_type==face_type).ToList();
        }

        #endregion
    }
}
