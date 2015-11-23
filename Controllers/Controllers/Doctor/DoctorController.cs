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

namespace Controllers
{
    public class DoctorController : Controller
    {
        #region 查询提现申请列表
        public ActionResult CashdrawList(int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;
            //状态
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CashdrawStatus))
                .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;

            string error = string.Empty;
            List<md_cashdraw_app> resultlist = new md_cashdraw_app_Bll().SearchCashdrawList(1, null, null, null, null, null, 0, null, null, pageIndex.Value, 20, out error);
            return View();
        }

        public ActionResult CashdrawList(int statu, DateTime? date1, DateTime? date2, int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;
            //状态
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(CashdrawStatus))
                .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;


            string error = string.Empty;
            List<md_cashdraw_app> resultlist = new md_cashdraw_app_Bll().SearchCashdrawList(1, date1, date2, null, null, null, statu, null, null, pageIndex.Value, 20, out error);
            return View();
        }

        #endregion

        #region 处理提现申请

        public ActionResult UpdateCashdrawList()
        {
            var ids = new List<int>();
            md_cashdraw_app_Bll cashdrawBll = new md_cashdraw_app_Bll();
            md_docter_Bll doctorBll = new md_docter_Bll();
            List<md_cashdraw_app> cashdrawlist = cashdrawBll.GetCashdrawByIds(ids);
            string error = string.Empty;
            foreach (var item in cashdrawlist)
            {
                item.opstatus = (int)Model.ConfigClass.CashdrawStatus.已处理;
                cashdrawBll.UpdateCashdraw(item, out error);
            }

            var doctorids = cashdrawlist.Select(m => m.drid).Distinct().ToList();
            var doctorlist = doctorBll.GetDoctorByIds(doctorids);

            foreach (var item in doctorlist)
            {
                var list = cashdrawlist.Where(m => m.drid == item.ToString()).ToList();
                if (list.Any())
                {
                    var money = list.Select(m => m.drawmoney).Sum();
                    item.current_income -= money;
                    doctorBll.UpdateDocter(item, out error);

                    md_dr_account accountinfo = new md_dr_account();
                    accountinfo.Initial();
                    accountinfo.dr_id = item.pkid;
                    accountinfo.income_type = 0;
                    accountinfo.money = money;

                    md_account_Bll accountBll = new md_account_Bll();
                    accountBll.CreateAccount(accountinfo,out error);
                }
            }
            return View();
        }

        #endregion
    }
}
