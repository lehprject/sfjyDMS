using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;
using System.Reflection;

namespace Controllers.Helpers
{
    /// <summary>
    /// SelectHelper 的摘要说明
    /// </summary>
    public class SelectHelper
    {
        public SelectHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public static List<vm_SelectInfo> ToViewModelList<T>(List<T> list, string idProperty, string textProperty)
        {
            List<vm_SelectInfo> result = new List<vm_SelectInfo>();

            Type tyInfo = typeof(T);
            PropertyInfo idInfo = tyInfo.GetProperty(idProperty);
            PropertyInfo textInfo = tyInfo.GetProperty(textProperty);
            foreach (var item in list)
            {
                result.Add(new vm_SelectInfo() { id = idInfo.GetValue(item).ToString(), text = textInfo.GetValue(item).ToString() });
            }

            return result;
        }

        public static List<vm_SelectInfo> ToViewModelList<T>(List<T> list, string idProperty, string textProperty, params string[] additionItems)
        {
            List<vm_SelectInfo> result = new List<vm_SelectInfo>();

            Type tyInfo = typeof(T);
            //key-value
            PropertyInfo idInfo = tyInfo.GetProperty(idProperty);
            PropertyInfo textInfo = tyInfo.GetProperty(textProperty);
            List<PropertyInfo> additionInfoList = new List<PropertyInfo>();

            //addition
            if (additionItems != null)
            {
                foreach (var addition in additionItems)
                {
                    additionInfoList.Add(tyInfo.GetProperty(addition));
                }
            }

            foreach (var item in list)
            {
                //key-value
                var selectInfo = new vm_SelectInfo() { id = idInfo.GetValue(item).ToString(), text = textInfo.GetValue(item).ToString() };

                //addition
                selectInfo.additionitems = new Dictionary<string, string>();
                foreach (var additionInfo in additionInfoList)
                {
                    if (additionInfo == null) continue;
                    selectInfo.additionitems.Add(additionInfo.Name, additionInfo.GetValue(item).ToString());
                }
                result.Add(selectInfo);
            }

            return result;
        }
    }
}
