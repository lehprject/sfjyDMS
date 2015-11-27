using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using System.Net.Http.Formatting;
using System.Net.Http;

namespace webApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //----------- Web API configuration and services -----------

            //如果不需要XML格式的数据,则去掉下面的注释
            //config.Formatters.Remove(config.Formatters.XmlFormatter); 

            //JSON 格式化
            config.Formatters.Remove(config.Formatters.JsonFormatter); 
            var jsonFormatter = new FieldsJsonMediaTypeFormatter();

            #region SerializerSettings
            var jsonResolve = new Share.IgnorableSerializerContractResolver();
            jsonResolve.Ignore(typeof(Model.md_user), "loginpwd");
            jsonResolve.Ignore(typeof(Model.dr_visit), "pkid");

            jsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = jsonResolve,
            };
            #endregion 

            config.Formatters.Add(jsonFormatter);

            //----------- Web API routes -----------
            config.MapHttpAttributeRoutes();

            /* 注意
             * 
             * 1)
             * 只能定义（最多）一个符合以下条件的函数 
             * a）相同 GET/POST/PUT/DELETE动作的,
             * b）只有一个名为id的参数
             * 也就是,当定义类似SomeFunction(int id)的函数时,就需注意了。
             * 
             * 2)
             * 只能定义（最多一个）具有相同动作的无参函数,如Get()和GetSome() 会造成冲突
             * 
             * 3)可空的参数也参与路由选择
             * 例如 SomeFunction(int? p1)
             * 如果 使用api/action/SomeFunction,不能进行路由
             *      要使用api/action/SomeFunction?p1=10
             *      或者  api/action/SomeFunction?p1
             **/

            config.Routes.MapHttpRoute(
                name: "ActionMethod",
                //如果使用 "api/{controller}/{action}",则DefaultApi就被覆盖
                //所以使用了"api/{controller}/action/{action}"
                //{fields}:指定返回所需对象的属性,多个属性用","（逗号）隔开
                routeTemplate: "api/{controller}/action/{action}/{fields}",
                defaults: new { action = RouteParameter.Optional, fields = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    } 

}
