using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Idr_message_Bll
    {
        #region 添加
        dr_message CreateDrMessage(dr_message info, out string error);
        #endregion
    }
}
