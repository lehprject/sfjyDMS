using System;
using System.Collections.Generic;
using System.Linq; 

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using System.Net.Http.Formatting;
using System.Net.Http;

namespace webApi
{ 

    #region 自定义JsonMediaTypeFormatter
    public class FieldsJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {

        public System.Net.Http.HttpRequestMessage CurrentRequest
        {
            get;
            private set;
        }

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request,
            System.Net.Http.Headers.MediaTypeHeaderValue mediaType)
        {
            //所需的对象属性
            var includingFields = request.GetRouteData().Values["fields"];
            if (includingFields != null && !string.IsNullOrEmpty(includingFields.ToString()))
            {
                FieldsJsonMediaTypeFormatter frmtr = new FieldsJsonMediaTypeFormatter();
                frmtr.CurrentRequest = request;
                var resolve = new Share.IncludableSerializerContractResolver(this.SerializerSettings.ContractResolver as Share.IgnorableSerializerContractResolver);
                //type.IsAssignableFrom(typeof(IEnumerable<Model.dr_pre_visit>))
                if (type.GetInterface("IEnumerable") != null)
                {
                    resolve.Include(type.GenericTypeArguments[0], includingFields.ToString(), ',');
                }
                else
                {
                    resolve.Include(type, includingFields.ToString(), ",");
                }

                frmtr.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = resolve,
                };
                return frmtr;
            }
            else
            {
                return this;
            }


        }
    }
    #endregion

}
