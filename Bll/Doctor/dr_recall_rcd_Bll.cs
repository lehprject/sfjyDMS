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
    public class dr_recall_rcd_Bll : Idr_recall_rcd_Bll
    {
        #region 添加
        public dr_recall_rcd CreateDrRecallRcd(dr_recall_rcd info, out string error)
        {
            error = string.Empty;
            try
            {
                dr_recall_rcd_DA da = new dr_recall_rcd_DA();
                da.CreateDrRecallRcd(info, out error);
                return info;
            }
            catch (Exception ex)
            {
                error += Share.BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }
        #endregion
    }
}
