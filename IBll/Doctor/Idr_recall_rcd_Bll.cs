using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Idr_recall_rcd_Bll
    {
        #region 添加
        dr_recall_rcd CreateDrRecallRcd(dr_recall_rcd info, out string error);
        #endregion
    }
}
