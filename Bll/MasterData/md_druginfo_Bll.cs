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
    public class md_druginfo_Bll
    {
        #region 查询

        /// <summary>
        /// 药品查询
        /// </summary>
        /// <param name="drugname">药品名</param>
        /// <param name="standard">规格</param>
        /// <param name="producerid">厂家id</param>
        /// <param name="cust_name">科组</param>
        /// <param name="sub_name">病种</param>
        /// <param name="orderby"></param>
        /// <param name="orderbyCol"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<md_druginfo> SearchDruginfoList(string drugname, string standard, int producerid, string cust_name, string sub_name,
            orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                md_druginfo_DA da = new md_druginfo_DA();
                var resultList = da.SearchDruginfoList(drugname, standard, producerid, cust_name, sub_name,
             orderby, orderbyCol, pageIndex, pageSize, out error);
                return resultList;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }
        #endregion
    }
}
