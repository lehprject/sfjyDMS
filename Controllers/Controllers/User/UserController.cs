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
    public class UserController:Controller
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
                //欢迎登录页
                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "系统设置", ParentId = 0, Url = "" };
                    list.Add(mainInfo);

                    int order = 1;
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "欢迎页", ParentId = mainInfo.FunctionId, Url = "/Index", Order = order++ });
                }
                 

                //产品管理
                {
                    RoleAuthorization mainInfo = new RoleAuthorization() { Level = 1, FunctionId = functionId++, FunctionName = "产品管理", ParentId = 0, Url = "" };
                    list.Add(mainInfo);
                    int order = 1;
                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "产品资料查询", ParentId = mainInfo.FunctionId, Url = "/master/ProductList", Order = order++ });

                    {
                        RoleAuthorization subInfo = new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "产品资料维护", ParentId = mainInfo.FunctionId, Url = "/master/EditProductList", Order = order++ };
                        list.Add(subInfo);


                        int subOrder = 1;
                        list.Add(new RoleAuthorization() { Level = 3, FunctionId = functionId++, FunctionName = "新增产品", ParentId = subInfo.FunctionId, Url = "/master/NewProduct", ReferenceUrl = subInfo.Url, Order = subOrder++ });
                    }

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "产品上架", ParentId = mainInfo.FunctionId, Url = "/master/UploadProducts", Order = order++ });

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "新品申请", ParentId = mainInfo.FunctionId, Url = "/master/NewProductApp", Order = order++ });

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "产品审批", ParentId = mainInfo.FunctionId, Url = "/master/NewProductChk", Order = order++ });

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "连锁产品库", ParentId = mainInfo.FunctionId, Url = "/master/MyProductList", Order = order++ });

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "缺货管理", ParentId = mainInfo.FunctionId, Url = "/master/UpdateProducts", Order = order++ });

                    list.Add(new RoleAuthorization() { Level = 2, FunctionId = functionId++, FunctionName = "产品关联", ParentId = mainInfo.FunctionId, Url = "/master/ProductRelated", Order = order++ });
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
