using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBll;
using Dal;
using Model;
using Model.ConfigClass;
using Share;

namespace Bll
{
    public class dr_message_Bll : Idr_message_Bll
    {

        #region 添加
        public dr_message CreateDrMessage(dr_message info, out string error)
        {
            error = string.Empty;
            try
            {
                dr_message_DA da = new dr_message_DA();
                da.CreateDrMessage(info, out error);
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
