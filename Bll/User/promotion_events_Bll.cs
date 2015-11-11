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
        public List<promotion_events> GetPromotionEventList(int face_type)
        {
            return new promotion_events_DA().GetPromotionEventList(face_type);
        }
    }
}
