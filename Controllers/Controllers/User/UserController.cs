using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Model;
using Model.ViewModel;
using Newtonsoft.Json;

namespace Controllers
{
    public class UserController : Controller
    {
        #region Container页面

        public ActionResult Container()
        {

            Helpers.RoleAuthorizationHelper authHelper = new Helpers.RoleAuthorizationHelper(AuthorizationList);
            ViewBag.configJson = JsonConvert.SerializeObject(authHelper.ToBuiConfigList(AuthorizationList));
            ViewBag.topmanuList = AuthorizationList.FindAll(t => t.Level == 1);


            return View();
        }

        private string urlPrefix;

        private string UrlPrefix
        {
            get
            {
                if (urlPrefix == null)
                {
                    try
                    {
                        urlPrefix = System.Configuration.ConfigurationManager.AppSettings["UrlPrefix"];
                        if (!urlPrefix.StartsWith("/") && !string.IsNullOrEmpty(urlPrefix))
                            urlPrefix = "/" + urlPrefix;
                    }
                    catch (Exception ex)
                    {
                        urlPrefix = string.Empty;
                    }
                }
                return urlPrefix;
            }
        }

        private List<RoleAuthorization> AuthorizationList
        {
            get
            {
                List<RoleAuthorization> list = new List<RoleAuthorization>();
                #region 构造测试数据

                int functionId = 1;

                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "系统管理", ParentId = 0, Url = "" };
                    list.Add(mainInfo);

                    int order = 1;
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "二维码管理", ParentId = mainInfo.FunctionId, Url = "/Index", Order = order++ });
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "提现管理", ParentId = mainInfo.FunctionId, Url = "/Doctor/cashdrawList", Order = order++ });

                }


                //助理平台
                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "助理工作台", ParentId = 0, Url = "" };
                    list.Add(mainInfo);
                    int order = 1;
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "附件审核", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "病历管理", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "检查单", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "处方单", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "留言管理", ParentId = mainInfo.FunctionId, Url = "/master/NewProductApp", Order = order++ });
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "回拨记录", ParentId = mainInfo.FunctionId, Url = "/master/NewProductApp", Order = order++ });

                }
                //医生管理
                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "医生管理", ParentId = 0, Url = "" };
                    list.Add(mainInfo);
                    int order = 1;

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "医生列表", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });
                }
                //患者管理
                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "患者管理", ParentId = 0, Url = "" };
                    list.Add(mainInfo);
                    int order = 1;

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "患者列表", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });
                }
                //活动管理
                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "活动管理", ParentId = 0, Url = "" };
                    list.Add(mainInfo);
                    int order = 1;

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "活动列表", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });
                }

                Helpers.RoleAuthorizationHelper helper = new Helpers.RoleAuthorizationHelper(list);
                list.FindAll(t => t.Level == 1).ForEach(
                    t =>
                    {
                        var subItem = helper.GetDescendants(t.FunctionId).FindAll(s => !string.IsNullOrEmpty(s.Url)).OrderBy(s => s.Order).OrderBy(s => s.Level).FirstOrDefault();
                        t.Url = subItem == null ? string.Empty : subItem.Url;
                    });
                list.RemoveAll(t => t.Level == 1 && string.IsNullOrEmpty(t.Url));

                list.FindAll(t => !string.IsNullOrEmpty(t.Url)).ForEach(t => t.Url = UrlPrefix + t.Url);
                #endregion
                return list;
            }
        }

        #endregion
    }
}
