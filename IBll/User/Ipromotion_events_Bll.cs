using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Ipromotion_events_Bll
    {
        List<promotion_events> GetPromotionEventList(int face_type);
    }
}
