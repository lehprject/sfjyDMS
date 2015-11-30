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
    public class PatientMessageController : Controller
    {
        #region 查询患者留言列表

        public ActionResult PatientMessageList()
        {
            List<SelectListItem> status = (Share.EnumOperate.ToKeyValues(typeof(MessageReplyType))
               .Select(t => new SelectListItem() { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == 0 })).ToList();
            ViewBag.status = status;
            return View();
        }

        public JsonResult PatientMessages(int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;

            string error = string.Empty;
            int record = 0;
            List<patient_message> resultlist = new patient_message_Bll().SearchMessgaeList(0, 0, 0, 0, null, null,null,
             null,null, pageIndex.Value, 20, out error);
            if (resultlist != null)
            {
                PagingResponse<patient_message> pagingResponse = new PagingResponse<patient_message>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            return null;
        }

        public JsonResult SearchPatientMessages(int hispital_id, DateTime? date1, DateTime? date2, int? statu = 1, int? pageIndex = 1)
        {
            if (pageIndex <= 1)
                pageIndex = 1;

            string error = string.Empty;
            int record = 0;
            List<patient_message> resultlist = new patient_message_Bll().SearchMessgaeList(hispital_id,
             0, 0, statu.Value,null,  null, null,null, null, pageIndex.Value, 20, out error);
            if (resultlist != null)
            {
                PagingResponse<patient_message> pagingResponse = new PagingResponse<patient_message>();
                pagingResponse.TotalRecord = record;
                pagingResponse.ResultList = resultlist;
                var jsonResult = Json(pagingResponse, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }

            return null;
        }

        #endregion

        #region 创建回复患者留言记录

        public ActionResult CreatePatientMessageRcd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePatientMessageRcd(FormCollection collection)
        {
            var content = collection["content"];
            int msid = 0;
            patient_message_rcd info = new patient_message_rcd();
            info.Initial();

            Base_Bll bll = new Base_Bll();
            var message=bll.GetInfo<patient_message>(msid);

            info.msg_id = message.pkid;
            info.rcd_contents = content;
            info.createuser = string.Empty;//回复人：助理/医生名字

            string error = string.Empty;
            new patient_message_Bll().CreateMessageRcd(info,out error);
            if(!string.IsNullOrEmpty(error))
            {
                ViewBag.ErrorMessage = "回复失败";               
            }
            return View(info);
        }
        #endregion
    }
}
