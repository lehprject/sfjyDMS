using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;

namespace Model
{
    /// <summary>
    /// A表字段A.someID与B表字段B.someName的关系
    /// </summary>
    public class EntityRelationInfo
    {

        public EntityRelationInfo(Type TheTable, Type RefTable, string TheColumn, string RefColumn, string JoinColumn, string ParentColumn)
        {
            this.TheTable = TheTable;
            this.RefTable = RefTable;

            TheColumnInfo = TheTable.GetProperty(TheColumn);
            JoinColumnInfo = TheTable.GetProperty(JoinColumn);


            this.TheColumn = TheColumn;
            this.JoinColumn = JoinColumn;
            this.ParentColumn = ParentColumn;
            this.RefColumn = RefColumn;

        }

        public Type TheTable { get; set; }

        public Type RefTable { get; set; }

        public PropertyInfo TheColumnInfo { get; set; }
        public PropertyInfo JoinColumnInfo { get; set; }

        public string TheColumn { get; set; }
        public string RefColumn { get; set; }

        public string JoinColumn { get; set; }

        public string ParentColumn { get; set; }
    }

    /// <summary>
    /// 关联查询Helper
    /// </summary>
    public static class EntityConfigContainer
    {
        static EntityConfigContainer()
        {

            #region patient_recipelist
            EntityConfigContainer.SetupFor<patient_recipelist>(() =>
            {
                EntityConfigContainer.ForMember<patient_recipelist, patient_info>(t => t.patient_name, r => r.name,
                    t => t.patient_id, r => r.pkid);

                EntityConfigContainer.ForMember<patient_recipelist, patient_info>(t => t.gender, r => r.gender,
                   t => t.patient_id, r => r.pkid);

                EntityConfigContainer.ForMember<patient_recipelist, patient_info>(t => t.birth, r => r.birthday,
                   t => t.patient_id, r => r.pkid);

                EntityConfigContainer.ForMember<patient_recipelist, patient_info>(t => t.alllergic_his, r => r.alllergic_his,
                t => t.patient_id, r => r.pkid);
            });
            #endregion


            #region patient_recipelist_druguse
            EntityConfigContainer.SetupFor<patient_recipelist_druguse>(() =>
            {
                EntityConfigContainer.ForMember<patient_recipelist_druguse, md_druginfo>(t => t.commonname, r => r.commonname,
                    t => t.drugid, r => r.pkid);

                EntityConfigContainer.ForMember<patient_recipelist_druguse, md_druginfo>(t => t.standard, r => r.standard,
                   t => t.drugid, r => r.pkid);

            });
            #endregion
        }

        #region Method

        #region Public Method
        /// <summary>
        /// action中应调用ForMember函数,
        /// 在对T第一次调用GetFor函数时,才会执行该action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setupAc"></param>
        public static void SetupFor<T>(Action setupAc)
        {
            Type typeInfo = typeof(T);
            if (SetUpdateActionList.Any(t => t.Key == typeInfo) == false)
                SetUpdateActionList.Add(typeInfo, setupAc);
        }

        /// <summary>
        /// 设置关系
        /// 效果:
        /// T.theColumn = R.refColumn
        /// T 和 R JOIN BY T.joinColumn = R.parentColumn
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="theColumn">传参方式:t=> t.属性, 该属性需要关联表R.refColumn</param>
        /// <param name="refColumn">传参方式:r=> r.属性</param>
        /// <param name="joinColumn">传参方式:t=> t.属性,T表和R表关联的字段,用于SQL中的JOIN条件</param>
        /// <param name="parentColumn">传参方式:r=> r.属性,T表和R表关联的字段,用于SQL中的JOIN条件</param>
        public static void ForMember<T, R>(Expression<Func<T, object>> theColumn, Expression<Func<R, object>> refColumn,
           Expression<Func<T, object>> joinColumn, Expression<Func<R, object>> parentColumn)
        {
            string TheColumn = string.Empty, RefColumn = string.Empty, JoinColumn = string.Empty, ParentColumn = string.Empty;

            TheColumn = GetPropertyName<T>(theColumn);
            RefColumn = GetPropertyName<R>(refColumn);
            JoinColumn = GetPropertyName<T>(joinColumn);
            ParentColumn = GetPropertyName<R>(parentColumn);

            EntityRelationInfo relationInfo = new EntityRelationInfo(typeof(T), typeof(R), TheColumn, RefColumn, JoinColumn, ParentColumn);
            RelationList.Add(relationInfo);
        }

        public static T GetFor<T>(T info, Expression<Func<T, object>> selector)
        {
            var list = new List<T>();
            list.Add(info);
            GetFor(list, selector);
            return list.FirstOrDefault();
        }

        public static IEnumerable<T> GetFor<T>(IEnumerable<T> list, Expression<Func<T, object>> selector)
        {
            List<string> columnList = GetPropertyNames(selector);
            if (columnList == null || columnList.Count == 0)
                return list;

            List<EntityRelationInfo> theRelationList = GetRelationFor<T>();
            if (theRelationList == null || theRelationList.Count == 0)
                return list;

            theRelationList = theRelationList.FindAll(t => columnList.Any(c => c == t.TheColumn));
            if (theRelationList.Count == 0)
                return list;

            //分组:关联相同的表 和 使用相同的关联字段,分为一组
            var groups = theRelationList.GroupBy(t => new { t.RefTable, t.JoinColumnInfo, t.ParentColumn });
            var groupsList = groups.ToList();

            //如果A关联B需要通过A.joinKey1,而A.joinKey1需要关联C获取,则需要先执行A与C的关联
            var dependentJoinList = theRelationList.FindAll(t => theRelationList.Any(d => d.TheColumn == t.JoinColumn));
            if (dependentJoinList.Count > 0)
            {
                foreach (var eachDependency in dependentJoinList)
                {
                    var theGroups = groups.Where(t => t.Key.JoinColumnInfo == eachDependency.JoinColumnInfo && t.Key.RefTable == eachDependency.RefTable);
                    if (theGroups != null && theGroups.Count() > 0)
                    {
                        groupsList.RemoveAll(t => t.Key.JoinColumnInfo == eachDependency.JoinColumnInfo);
                        groupsList.AddRange(theGroups);
                    }
                }
            }

            //获取关联属性的值
            foreach (var group in groupsList)
            {
                var eachList = list.Where(t => group.Key.JoinColumnInfo.GetValue(t) != null);
                Execute(eachList, group.Key.RefTable, group.Key.JoinColumnInfo, group.Key.ParentColumn, group.ToList());
            }

            return list;
        }

        #endregion

        #region private Method
        private static List<string> GetPropertyNames<TModel>(System.Linq.Expressions.Expression<Func<TModel, object>> selector)
        {
            List<string> resultList = new List<string>();
            try
            {
                string propertyName = GetPropertyName<TModel>(selector);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    //单个属性
                    resultList.Add(propertyName);
                }
                else
                {
                    //多个属性
                    System.Linq.Expressions.NewExpression nbody = (System.Linq.Expressions.NewExpression)selector.Body;
                    resultList = nbody.Members.Select(t => t.Name).ToList();
                }

                return resultList;
            }
            catch (Exception ex)
            {
                return resultList;
            }
        }

        private static string GetPropertyName<TModel>(System.Linq.Expressions.Expression<Func<TModel, object>> selector)
        {
            try
            {
                string propertyName = string.Empty;
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
                    propertyName = body.Member.Name;
                }

                return propertyName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 获取类型T的关联查询的关系列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<EntityRelationInfo> GetRelationFor<T>()
        {
            Type type = typeof(T);
            if (RelationList.Any(t => t.TheTable == type))
                return RelationList.FindAll(t => t.TheTable == type);
            else
            {
                if (SetUpdateActionList.ContainsKey(type))
                {
                    SetUpdateActionList.FirstOrDefault(t => t.Key == type).Value();
                    return RelationList.FindAll(t => t.TheTable == type);
                }

                return null;
            }
        }

        /// <summary>
        /// 执行:生成sql,查询,赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="refTable"></param>
        /// <param name="joinColumnInfo"></param>
        /// <param name="parentColumn"></param>
        /// <param name="relationList"></param>
        private static void Execute<T>(IEnumerable<T> list, Type refTable, PropertyInfo joinColumnInfo, string parentColumn, List<EntityRelationInfo> relationList)
        {
            #region sql
            var selectColumns = relationList.Select(t => t.RefColumn).Union(new string[] { parentColumn });
            string selectStr = string.Join(",", selectColumns);
            string whereStr = "'" + string.Join(",", list.Select(t => joinColumnInfo.GetValue(t).ToString()).Distinct()) + "'";


            string sql = string.Format(" select {0} from {1} where FIND_IN_SET( {2} , {3} );  ",
                selectStr, refTable.Name, parentColumn, whereStr);
            #endregion

            #region db
            //依赖Mycat
            Model.MySqlHelper sqlHelper = new Model.MySqlHelper();
            DataTable dt = sqlHelper.ExecuteDataTable(sql);

            //不依赖Mycat 按表选择连接
            //DBHelper dbhelper = new DBHelper();
            //var sqlHelper = dbhelper.GetMysqlHelperFor<T>();
            //DataTable dt = sqlHelper.ExecuteDataTable(sql);
            #endregion

            #region assign
            if (dt != null)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    var eachRow = dt.Rows[i];
                    string parentValue = eachRow[parentColumn] == null ? null : eachRow[parentColumn].ToString();
                    var eachList = list.Where(t => joinColumnInfo.GetValue(t).ToString() == parentValue);

                    ////不能直接比较
                    //object a1 = joinColumnInfo.GetValue(list.First());
                    //var a2 = eachRow[parentColumn];
                    //bool success = a1  == a2;//false
                    ////

                    if (eachList == null || eachList.Count() == 0)
                        continue;


                    foreach (var eachRelation in relationList)
                    {
                        var theValue = dt.Rows[i][eachRelation.RefColumn];
                        foreach (var eachInfo in eachList)
                        {
                            try
                            {
                                eachRelation.TheColumnInfo.SetValue(eachInfo, theValue);
                            }
                            catch { }
                        }
                    }
                }
            }
            #endregion
        }

        #endregion

        #endregion

        #region Field
        /// <summary>
        /// 设置关联列表的函数
        /// </summary>
        private static Dictionary<Type, Action> SetUpdateActionList = new Dictionary<Type, Action>();

        /// <summary>
        /// 关联查询的关系列表
        /// </summary>
        private static List<EntityRelationInfo> RelationList = new List<EntityRelationInfo>();
        #endregion
    }
}
