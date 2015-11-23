using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModel;

namespace Controllers.Helpers
{
    public class RoleAuthorizationHelper
    {
        public RoleAuthorizationHelper(List<RoleAuthorization> authList)
        {
            this.AuthList = authList;
        }
        public List<RoleAuthorization> AuthList { get; set; }
        public RoleAuthorization GetRoleAuthoriztionInfo(int functionId)
        {
            return AuthList.FirstOrDefault(t => t.FunctionId == functionId);
        }

        public List<RoleAuthorization> GetDescendants(int functionId)
        {
            List<RoleAuthorization> resultList = new List<RoleAuthorization>();
            List<RoleAuthorization> childList = AuthList.FindAll(t => t.ParentId == functionId);
            resultList.AddRange(childList);

            foreach (var item in childList)
            {
                var theChildList = GetDescendants(item.FunctionId);
                resultList.AddRange(theChildList);
            }

            return resultList;
        }

        public RoleAuthorization GetTopAuthorization(List<RoleAuthorization> list, string url)
        {
            RoleAuthorization info = list.FirstOrDefault(t => t.Url.ToLower() == url.ToLower());

            if (info != null)
            {
                //Func<RoleAuthorization,RoleAuthorization> rf =
                //    (RoleAuthorization item) => 
                //    {
                //        var parent = list.FirstOrDefault(t=>t.FunctionId == item.ParentId); 
                //        if(parent == null)
                //            return null;
                //        if(parent.Level == 1)
                //            return parent;

                //        return rf(parent);
                //    };


                return GetTopAuthorization(list, info);
            }

            return null;
        }

        public RoleAuthorization GetTopAuthorization(List<RoleAuthorization> list, RoleAuthorization info)
        {
            var parent = list.FirstOrDefault(t => t.FunctionId == info.ParentId);
            if (parent == null)
                return null;
            if (parent.Level == 1)
                return parent;

            return GetTopAuthorization(list, parent);
        }

        #region BUI

        public List<vm_BUI_config> ToBuiConfigList(List<RoleAuthorization> authList)
        {
            List<vm_BUI_config> resultList = new List<vm_BUI_config>();

            var topLevelList = authList.FindAll(t => t.Level == 1);
            foreach (var topInfo in topLevelList)
            {
                var secondLevelList = authList.FindAll(t => t.Level == 2 && t.ParentId == topInfo.FunctionId);
                if (secondLevelList.Count == 0)
                    continue;

                vm_BUI_config configInfo = new vm_BUI_config();
                configInfo.id = topInfo.FunctionId.ToString();
                configInfo.homePage = secondLevelList.OrderBy(t => t.Order).FirstOrDefault().FunctionId.ToString();

                vm_bui_menuContainer containerInfo = new vm_bui_menuContainer();
                containerInfo.text = topInfo.FunctionName;
                containerInfo.items = new List<vm_bui_menu>();
                foreach (var secondmenu in secondLevelList)
                {
                    vm_bui_menu theMenuInfo = new vm_bui_menu();
                    theMenuInfo.href = secondmenu.Url;
                    theMenuInfo.id = secondmenu.FunctionId.ToString();
                    theMenuInfo.text = secondmenu.FunctionName;
                    containerInfo.items.Add(theMenuInfo);
                }

                configInfo.menu = new List<vm_bui_menuContainer>();
                configInfo.menu.Add(containerInfo);

                resultList.Add(configInfo);
            }

            return resultList;
        }

        
        #endregion
    }
}
