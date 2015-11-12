using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Model;
using Model.ConfigClass;
using Share;

namespace Dal
{
    public class patient_info_DA : DataAccessBase
    {
        #region 患者病种
        /// <summary>
        /// 查询患者病种信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="gender"></param>
        /// <param name="cardtype"></param>
        /// <param name="cardno"></param>
        /// <param name="createtime"></param>
        /// <param name="alllergic_his"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<patient_disease> SearchPatientDiseaseList(string name, string mobile, string gender, string cardtype, string cardno, DateTime? createtime,
            string alllergic_his,out string error)
        {
            error = string.Empty;
            try
            {
                #region Command

                string selectSql = string.Format("select * from patient_disease d left join patient_info info on d.patient_id=info.pkid WHERE TRUE ");

                StringBuilder conditionSb = new System.Text.StringBuilder();

                List<MySqlParameter> paraList = new List<MySqlParameter>();
                #endregion

                #region 搜索条件
                if (!string.IsNullOrEmpty(name))
                {
                    conditionSb.Append(" AND info.name = @name ");
                    paraList.Add(new MySqlParameter("name", name));
                }

                if (!string.IsNullOrEmpty(mobile))
                {
                    conditionSb.Append(" AND info.mobile = @mobile ");
                    paraList.Add(new MySqlParameter("mobile", mobile));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    conditionSb.Append(" AND info.gender = @gender ");
                    paraList.Add(new MySqlParameter("gender", gender));
                }

                if (!string.IsNullOrEmpty(cardtype))
                {
                    conditionSb.Append(" AND info.cardtype = @cardtype ");
                    paraList.Add(new MySqlParameter("cardtype", cardtype));
                }

                if (!string.IsNullOrEmpty(cardno))
                {
                    conditionSb.Append(" AND info.cardno = @cardno ");
                    paraList.Add(new MySqlParameter("cardno", cardno));
                }


                if (createtime.HasValue)
                {
                    conditionSb.Append(" AND info.createtime = @createtime ");
                    paraList.Add(new MySqlParameter("createtime", createtime));
                }

                if (!string.IsNullOrEmpty(alllergic_his))
                {
                    conditionSb.Append(" AND info.alllergic_his = @alllergic_his ");
                    paraList.Add(new MySqlParameter("alllergic_his", alllergic_his));
                }

                conditionSb.Append(" group by d.sub_disease ");

                #endregion

                #region 排序 分页
                string orderbyStr = string.Empty;
                orderbyStr = " order by d.pkid ";

                selectSql += conditionSb.ToString() + orderbyStr;
                #endregion

                #region 执行

                var resultList = db.Database.SqlQuery<patient_disease>(selectSql, paraList.ToArray()).ToList();
                return resultList;
                #endregion
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return null;
            }
        }

        #endregion
    }
}
