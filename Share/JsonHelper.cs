using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace Share
{
    public  class JsonHelper
    {
        public static string DataContractJsonSerialize<T>(object obj)
        {
            string jsonStr = string.Empty;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                
                serializer.WriteObject(ms, obj);

                StringBuilder sb = new StringBuilder();

                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));

                jsonStr = sb.ToString();

            }

            return jsonStr;
        }


        public static T DeserializeByDataConstractSerializer<T>(string JsonStr) where T : class
        {
            T result = default(T);
            DataContractJsonSerializer serializer = null;
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(JsonStr)))
            {

                serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

                result = serializer.ReadObject(ms) as T;
            }
            return result;
        }


    }

    #region 拓展DefaultContractResolver
    /// <summary>
    /// 序列化时，对指定属性不进行序列化，用于返回敏感信息，如序列member_info时，ignore密码属性
    /// </summary>
    public class IgnorableSerializerContractResolver : DefaultContractResolver
    {
        public readonly Dictionary<Type, HashSet<string>> Ignores;

        public IgnorableSerializerContractResolver()
        {
            this.Ignores = new Dictionary<Type, HashSet<string>>();
        }

        #region 加入ignore的属性
        /// <summary>
        /// Explicitly ignore the given property(s) for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName">one or more properties to ignore.  Leave empty to ignore the type entirely.</param>
        public void Ignore(Type type, params string[] propertyName)
        {
            // start bucket if DNE
            if (!this.Ignores.ContainsKey(type)) this.Ignores[type] = new HashSet<string>();

            foreach (var prop in propertyName)
            {
                this.Ignores[type].Add(prop);
            }
        }



        /// <summary>
        ///  请调用Ignore(Type type, params string[] propertyName)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        private IgnorableSerializerContractResolver Ignore<TModel>(System.Linq.Expressions.Expression<Func<TModel, object>> selector)
        {
            //try 1
            System.Linq.Expressions.MemberExpression body = selector.Body as System.Linq.Expressions.MemberExpression;

            if (body == null)
            {
                //try 2 
                System.Linq.Expressions.UnaryExpression ubody = (System.Linq.Expressions.UnaryExpression)selector.Body;
                body = ubody.Operand as System.Linq.Expressions.MemberExpression;
            }

            if (body != null)
            {
                string propertyName = body.Member.Name;
                this.Ignore(typeof(TModel), propertyName);
                return this;
            }
            else
            {
                //try 3
                System.Linq.Expressions.NewExpression nbody = (System.Linq.Expressions.NewExpression)selector.Body;
                if (nbody != null)
                {
                    this.Ignore(typeof(TModel), nbody.Members.Select(t => t.Name).ToArray());
                }
            }


            throw new ArgumentException("Could not get property name", "selector");

        }

        #endregion

        /// <summary>
        /// Is the given property for the given type ignored?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool IsIgnored(Type type, string propertyName)
        {
            if (!this.Ignores.ContainsKey(type)) return false;

            // if no properties provided, ignore the type entirely
            if (this.Ignores[type].Count == 0) return true;

            return this.Ignores[type].Contains(propertyName);
        }

        /// <summary>
        /// The decision logic goes here
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            Newtonsoft.Json.Serialization.JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (this.IsIgnored(property.DeclaringType, property.PropertyName)
                // need to check basetype as well for EF -- @per comment by user576838
            || this.IsIgnored(property.DeclaringType.BaseType, property.PropertyName))
            {
                //影响序列化 object=>string
                property.ShouldSerialize = instance => { return false; };
                //序列化和反序列化都受影响
                property.Ignored = true;
                //完全忽略该属性
                return null;
            }

            return property;
        }

    }

    /// <summary>
    /// ignore 的优先级高于 include
    /// </summary>
    public class IncludableSerializerContractResolver : IgnorableSerializerContractResolver
    {

        protected readonly Dictionary<Type, HashSet<string>> Includes;
        public IncludableSerializerContractResolver()
        {
            Includes = new Dictionary<Type, HashSet<string>>();

        }

        public IncludableSerializerContractResolver(IgnorableSerializerContractResolver ignoreResolver)
            : this()
        {
            if (ignoreResolver.Ignores != null)
            {
                foreach (var item in ignoreResolver.Ignores)
                {
                    Ignores.Add(item.Key, item.Value);
                }
            }

        }

        public bool IsInclude(Type type, string propertyName)
        {
            if (!this.Includes.ContainsKey(type)) return false;

            if (this.Includes[type].Count == 0) return false;

            return this.Includes[type].Contains(propertyName);
        }

        #region 加入Include属性
        public void Include(Type type, string propertyName)
        {

            // start bucket if DNE
            if (!this.Includes.ContainsKey(type))
                this.Includes[type] = new HashSet<string>();

            this.Includes[type].Add(propertyName);

        }

        public void Include(Type type, params string[] propertyName)
        {
            // start bucket if DNE
            if (!this.Includes.ContainsKey(type))
                this.Includes[type] = new HashSet<string>();

            foreach (var prop in propertyName)
            {
                this.Includes[type].Add(prop);
            }
        }

        public void Include(Type type, string propertyNames, char seperator)
        {
            var propertyList = propertyNames.Split(seperator);
            // start bucket if DNE
            if (!this.Includes.ContainsKey(type))
                this.Includes[type] = new HashSet<string>();

            foreach (var prop in propertyList)
            {
                this.Includes[type].Add(prop);
            }
        }
        #endregion

        /// <summary>
        /// The decision logic goes here
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            Newtonsoft.Json.Serialization.JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (property != null && property.Ignored == false)
            {
                if (this.IsInclude(property.DeclaringType, property.PropertyName)
                    // need to check basetype as well for EF -- @per comment by user576838
                    || this.IsInclude(property.DeclaringType.BaseType, property.PropertyName))
                {
                    property.ShouldSerialize = instance => { return true; };
                    property.Ignored = false;
                }
                else
                {
                    property.ShouldSerialize = instance => { return false; };
                    property.Ignored = true;
                }
            }


            return property;
        }
    }
    #endregion
}
