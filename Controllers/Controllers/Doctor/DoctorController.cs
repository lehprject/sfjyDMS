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
    public class DoctorController : Controller
    {
        #region 查询提现申请列表

        public ActionResult CashdrawList()
        {
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CashdrawStatus))
               .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;
            return View();
        }

        public JsonResult Cashdraws(int? pageIndex = 1)
        {
            Base_Bll bll = new Base_Bll();
            var info = bll.GetInfo<md_cashdraw_app>(1);

            if (pageIndex <= 1)
                pageIndex = 1;
            //状态
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CashdrawStatus))
                .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;

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

        #region 处理提现申请

        public ActionResult UpdateCashdrawList(string idstr)
        {
            if (!string.IsNullOrEmpty(idstr))
            {
                var str = idstr.TrimEnd(',');
                md_cashdraw_app_Bll cashdrawBll = new md_cashdraw_app_Bll();
                md_docter_Bll doctorBll = new md_docter_Bll();
                List<md_cashdraw_app> cashdrawlist = cashdrawBll.GetCashdrawByIds(str);
                string error = string.Empty;
                foreach (var item in cashdrawlist)
                {
                    item.opstatus = (int)Model.ConfigClass.CashdrawStatus.已处理;  ///批注By Andy:更新一次就调用一次数据库连接，对数据库性能有影响，建议传入需要更新的ids,用update语句一次执行
                }
                cashdrawBll.UpdateChashdrawList(cashdrawlist, out error);

                var doctorids = cashdrawlist.Select(m => m.drid).Distinct().ToList();
                var doctorlist = doctorBll.GetDoctorByIds(doctorids);

                List<md_docter> doctors = new List<md_docter>();
                List<md_dr_account> dr_accountlist = new List<md_dr_account>();
                foreach (var item in doctorlist)
                {
                    var list = cashdrawlist.Where(m => m.drid == item.pkid.ToString()).ToList();
                    if (list.Any())
                    {
                        var money = list.Select(m => m.drawmoney).Sum();
                        item.current_income -= money;
                        doctors.Add(item);
                        md_dr_account accountinfo = new md_dr_account();
                        accountinfo.Initial();
                        accountinfo.dr_id = item.pkid;
                        accountinfo.income_type = 0;
                        accountinfo.money = money;
                        dr_accountlist.Add(accountinfo);
                    }
                }
                if (doctors.Any())
                {
                    doctorBll.UpdateChashdrawList(doctors, out error);
                }
                if (dr_accountlist.Any())
                {
                    new md_account_Bll().CreateAccountList(dr_accountlist, out error);
                }

            }

            return View();
        }

        #endregion
    }
}
