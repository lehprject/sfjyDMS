using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Model;
using Model.ViewModel;
using Newtonsoft.Json;
using System.Linq;
using Model.ConfigClass;
using System.Collections.Generic;
using System;
using Bll;
using Share;

namespace Controllers
{
    public class PromotionController : Controller
    {
        #region 活动
        #region 查询提现申请列表

        public ActionResult PromotionEventList()
        {
            return View();

            //List<promotion_events> prolist = new promotion_events_Bll().GetAllPromotionEventl();


            //if (ChainFormID > 0)
            //{
            //    md_chainform chainformInfo = baseBll.GetChainFormInfoByID(ChainFormID);

            //    //序列化字段
            //    IncludableSerializerContractResolver resolver = new IncludableSerializerContractResolver();
            //    resolver.Include(typeof(promotion_events), BaseTool.GetPropertyNames<promotion_events>(t => new { t.pkid, t.name }).ToArray());

            //    return JsonConvert.SerializeObject(chainformInfo, new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore, ContractResolver = resolver });
            //}
            //else
            //{
            //    return "null";
            //}
        }

        /// <summary>
        /// 活动模糊查询
        /// </summary>
        /// <param name="nameparams"></param>
        /// <param name="format"></param>
        /// <param name="selectColumns"></param>
        /// <param name="idName"></param>
        /// <param name="textName"></param>
        /// <returns></returns>
        protected JsonResult SearchPromotionNameList(string nameparams, string format, string selectColumns, string idName, string textName)
        {
            //参数
            nameparams = nameparams ?? string.Empty;
            selectColumns = selectColumns ?? string.Empty;
            idName =  idName ?? "pkid";
            textName = textName ?? "mname";
            format = format ?? string.Empty;

            //BLL
            int record=0;
            string error=string.Empty;
            promotion_events_Bll bll = new promotion_events_Bll();
            List<promotion_events> promotionList = bll.GetPromotionEventList(0, 0, null, null,null, null,
             null, null,null, null,1, 30, out record, out error);

            //返回
            string jsonResult = string.Empty;
            if (format == "select")
            {
                string[] columns = string.IsNullOrEmpty(selectColumns) ? null : selectColumns.Split(',').ToArray();
                var selectList = Helpers.SelectHelper.ToViewModelList<promotion_events>(promotionList, idName, textName, columns);
                jsonResult = JsonConvert.SerializeObject(selectList);
            }
            else
            {
                jsonResult = JsonConvert.SerializeObject(promotionList);
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult SearchPromotionList(int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;

            string error = string.Empty;
            int record = 0;
            List<md_cashdraw_app> resultlist = new md_cashdraw_app_Bll().SearchCashdrawList(1, null, null, null, null, null, 0, null, null, pageIndex.Value, 2, out record, out error);
            if (resultlist != null)
            {
                foreach (var item in resultlist)
                {
                    item.apptime = item.app_time.ToString("yyyy - MM - dd");
                    item._optime = item.optime.ToString("yyyy - MM - dd");
                }
                PagingResponse<md_cashdraw_app> pagingResponse = new PagingResponse<md_cashdraw_app>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }

        public JsonResult CashdrawList1(DateTime? date1, DateTime? date2, int? statu = 1, int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;
            //状态
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CashdrawStatus))
                .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;


            string error = string.Empty;
            int record = 0;
            List<md_cashdraw_app> resultlist = new md_cashdraw_app_Bll().SearchCashdrawList(1, date1, date2, null, null, null, statu.Value, null, null, pageIndex.Value, 2, out record, out error);
            if (resultlist != null)
            {
                foreach (var item in resultlist)
                {
                    item.apptime = item.app_time.ToString("yyyy - MM - dd");
                    item._optime = item.optime.ToString("yyyy - MM - dd");
                }


                PagingResponse<md_cashdraw_app> pagingResponse = new PagingResponse<md_cashdraw_app>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }

            return null;
        }

        #endregion 

        #endregion

        #region 优惠券
        #endregion

        #region 优惠券明细
        #endregion
    }
}
