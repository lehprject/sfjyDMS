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
using Newtonsoft.Json.Linq;

namespace Controllers
{
    public class PromotionController : Controller
    {
        #region 活动
        public ActionResult PromotionEventList()
        {
            return View();
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
        public JsonResult SearchPromotionNameList(string nameparams, string format, string selectColumns, string idName, string textName)
        {
            //参数
           nameparams = nameparams ?? string.Empty;
            selectColumns = selectColumns ?? string.Empty;
            idName = idName ?? "name";
            textName = textName ?? "name";
            format = format ?? string.Empty;

            //BLL
            int record = 0;
            string error = string.Empty;
            promotion_events_Bll bll = new promotion_events_Bll();
            List<promotion_events> promotionList = bll.GetPromotionEventList(nameparams, 0, 0, null, null, null, null,
             null, null, null, null, 1, 30, out record, out error);

            //返回
            string jsonResult = string.Empty;
            if (format == "select")
            {
                string[] columns = string.IsNullOrEmpty(selectColumns) ? null : selectColumns.Split(',').ToArray();
                var selectList = Helpers.SelectHelper.ToViewModelList<promotion_events>(promotionList, idName, textName, columns);
                //jsonResult = JsonConvert.SerializeObject(selectList);
            }
            else
            {
                //jsonResult = JsonConvert.SerializeObject(promotionList);
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchPromotionList(int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;

            string error = string.Empty;
            int record = 0;
            List<promotion_events> resultlist = new promotion_events_Bll().GetPromotionEventList(null, 0, 0, null, null, null, null,
             null, null, null, null, pageIndex.Value, 20, out record, out error);
            if (resultlist != null)
            {
                foreach (var item in resultlist)
                {
                    item.starttime = item.startdate.ToString("yyyy - MM - dd");
                    item.endtime = item.enddate.ToString("yyyy - MM - dd");
                }
                PagingResponse<promotion_events> pagingResponse = new PagingResponse<promotion_events>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }

        public JsonResult PostSearchPromotionList(string name, DateTime? date1, DateTime? date2, int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;

            string error = string.Empty;
            int record = 0;
            List<promotion_events> resultlist = new promotion_events_Bll().GetPromotionEventList(name, 0, 0, date1, date2, null, null,
             null, null, null, null, pageIndex.Value, 20, out record, out error);
            if (resultlist != null)
            {
                foreach (var item in resultlist)
                {
                    item.starttime = item.startdate.ToString("yyyy - MM - dd");
                    item.endtime = item.enddate.ToString("yyyy - MM - dd");
                }
                PagingResponse<promotion_events> pagingResponse = new PagingResponse<promotion_events>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }

        public ActionResult CreatePromotionEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePromotionEvent(FormCollection collection)
        {
            var name = collection["name"];
            var date1 = collection["date1"];
            var date2 = collection["date2"];
            var content = collection["content"];

            if(string.IsNullOrEmpty(date1)||string.IsNullOrEmpty(date2))
            {
                ViewBag.ErrorMessage = "请选择日期";
                return View();
            }

            promotion_events info = new promotion_events();
            info.Initial();
            info.name = name;
            info.hospital_id = 0;
            info.contents = content;
            info.face_type = 0; //面向对象
            info.startdate = BaseTool.GetDateTime(date1);
            info.enddate = BaseTool.GetDateTime(date2);
            info.attachfile = "";

            string error = string.Empty;
            promotion_events_Bll bll = new promotion_events_Bll();
            bll.CreatePromotion(info, out error);

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.ErrorMessage = "添加活动失败！";
            }
            return View(info);
        }

        #endregion

        #region 优惠券

        public ActionResult PromotionCouponsList()
        {
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CouponsStatus))
               .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;
            return View();
        }

        public JsonResult PromotionCoupons(int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;
            //状态
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CouponsStatus))
                .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;

            string error = string.Empty;
            int record = 0;
            List<promotion_coupons> resultlist = new promotion_events_Bll().GetPromotionCouponsList(null, 0, 0, null, null, null, null,
           null, null, pageIndex.Value, 20, out record, out error);
            if (resultlist != null)
            {
                //foreach (var item in resultlist)
                //{
                //    item.apptime = item.app_time.ToString("yyyy - MM - dd");
                //    item._optime = item.optime.ToString("yyyy - MM - dd");
                //}
                PagingResponse<promotion_coupons> pagingResponse = new PagingResponse<promotion_coupons>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }

        public JsonResult SearchPromotionCoupons(string name, int values, int issue_status, DateTime? startdate1, DateTime? startdate2, int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;
            //状态
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CouponsStatus))
                .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;

            string error = string.Empty;
            int record = 0;
            List<promotion_coupons> resultlist = new promotion_events_Bll().GetPromotionCouponsList(name, values, issue_status, startdate1, startdate2, null, null,
           null, null, pageIndex.Value, 20, out record, out error);
            if (resultlist != null)
            {
                //foreach (var item in resultlist)
                //{
                //    item.apptime = item.app_time.ToString("yyyy - MM - dd");
                //    item._optime = item.optime.ToString("yyyy - MM - dd");
                //}
                PagingResponse<promotion_coupons> pagingResponse = new PagingResponse<promotion_coupons>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }

   
        [HttpGet]
        public ActionResult GreatePromotionCoupons()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GreatePromotionCoupons(FormCollection collection)
        {
            ResponseMessage result = new ResponseMessage();
            var name = collection["name"];
            var count = collection["count"];
            var values = collection["values"];
            var date1 = collection["date1"];
            var date2 = collection["date2"];
            var activity = collection["activity"];
            var morethan = collection["morethan"];
            var value1 = collection["value1"];
            var lessthan = collection["lessthan"];
            var value2 = collection["value2"];

            #region 优惠券
            promotion_coupons info = new promotion_coupons();
            info.Initial();
            info.name = name;
            info.issue_num =BaseTool.GetIntNumber(count);
            info.values = BaseTool.GetIntNumber(values);
            info.startdate = BaseTool.GetDateTime(date1);
            info.enddate = BaseTool.GetDateTime(date2);
            info.events_id = BaseTool.GetIntNumber(activity);
            #endregion

            #region 条件
            List<promotion_coupons_usecase> usecasellist = new List<promotion_coupons_usecase>();
            if(BaseTool.GetIntNumber(morethan)>0&&BaseTool.GetIntNumber(value1)>0)
            {
                var usecase = new promotion_coupons_usecase();
                usecase.Initial();
                usecase.case_pamars = morethan;
                usecase.case_pamars_value = value1;
                usecasellist.Add(usecase);
            }
            if (BaseTool.GetIntNumber(lessthan) > 0 && BaseTool.GetIntNumber(value2) > 0)
            {
                var usecase = new promotion_coupons_usecase();
                usecase.Initial();
                usecase.case_pamars = morethan;
                usecase.case_pamars_value = value1;
                usecasellist.Add(usecase);
            }
            #endregion 

            #region 优惠券明细
            List<promotion_coupons_detail> detaillist = new List<promotion_coupons_detail>();
            for (int i = 0; i < info.issue_num; i++)
            {
                promotion_coupons_detail detail = new promotion_coupons_detail();
                detail.Initial();
                detail.code = string.Empty;
                detaillist.Add(detail);
            }
            #endregion

            promotion_events_Bll bll = new promotion_events_Bll();
            string error = string.Empty;
            bll.CreatePromotionCoupons(info, detaillist, usecasellist, out error);

            int record = 0;
            List<promotion_coupons_detail> list = bll.SearchPromotionCouponsList(info.pkid, 0, 0, 0, 0, null, null, null, null,
            null, null, 1, 1000, out record, out error);
            ViewBag.detaillist = list;

            result.bSuccess = true;
            return Json(result);
        }

        #endregion

        #region 优惠券明细

        public JsonResult SearchPromotionCouponsDetail(int coupons_id, int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;

            string error = string.Empty;
            int record = 0;
            List<promotion_coupons_detail> resultlist = new promotion_events_Bll().SearchPromotionCouponsList(coupons_id, 0, 0, 0, 0, null, null, null, null,
           null, null, pageIndex.Value, 20, out record, out error);
            if (resultlist != null)
            {
                //foreach (var item in resultlist)
                //{
                //    item.apptime = item.app_time.ToString("yyyy - MM - dd");
                //    item._optime = item.optime.ToString("yyyy - MM - dd");
                //}
                PagingResponse<promotion_coupons_detail> pagingResponse = new PagingResponse<promotion_coupons_detail>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }


        #endregion
    }
}
