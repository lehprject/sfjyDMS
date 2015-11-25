using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using AutoMapper;
using System.Linq.Expressions;
using Model;
namespace Model
{
    public static class DBExtents
    {
        #region initial entity
        public static void Initial<T>(this T vobj)
        {
            {
                md_cashdraw_app obj = vobj as md_cashdraw_app;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.drid = string.Empty;
                    obj.app_time = DateTime.Now;
                    obj.drawmoney = 0;
                    obj.in_bank = 0;
                    //obj.in_bank_acc = string.Empty;
                    obj.opuser = string.Empty;
                    obj.optime = DateTime.Now;
                    obj.opstatus = 0;
                    obj.opremark = string.Empty;
                    return;
                }
            }
            {
                dr_recall_rcd obj = vobj as dr_recall_rcd;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.drid = 0;
                    obj.createtime = DateTime.Now;
                    obj.opuser = string.Empty;
                    obj.optime = DateTime.Now;
                    obj.opstatus = 0;
                    return;
                }
            }
            {
                dr_message obj = vobj as dr_message;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.drid = 0;
                    obj.createtime = DateTime.Now;
                    obj.msg_content = string.Empty;
                    obj.attach_file1 = string.Empty;
                    obj.attach_file2 = string.Empty;
                    obj.attach_file3 = string.Empty;
                    obj.status = 0;
                    return;
                }
            }
            {
                md_dr_account obj = vobj as md_dr_account;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.dr_id = 0;
                    obj.createtime = DateTime.Now;
                    obj.income_type = 0;
                    obj.money = 0;
                    obj.remarks = string.Empty;
                    return;
                }
            }

            {
                promotion_events obj = vobj as promotion_events;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.name = string.Empty;
                    obj.contents = string.Empty;
                    obj.hospital_id = 0;
                    obj.face_type = 0;
                    obj.startdate = DateTime.Now;
                    obj.enddate = DateTime.Now;
                    obj.attachfile = string.Empty;
                    obj.createtime = DateTime.Now;
                    return;
                }
            }

            {
                promotion_coupons obj = vobj as promotion_coupons;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.name = string.Empty;
                    obj.values = 0;
                    obj.issue_num = 0;
                    obj.issue_status = 0;
                    obj.startdate = DateTime.Now;
                    obj.enddate = DateTime.Now;
                    obj.events_id = 0;
                    return;
                }
            }

            {
                promotion_coupons_detail obj = vobj as promotion_coupons_detail;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.coupons_id = 0;
                    obj.values = 0;
                    obj.code = string.Empty;
                    obj.use_status = 0;
                    obj.business_type = 0;
                    obj.userid = 0;
                    obj.drid = 0;
                    obj.sendtime = DateTime.Now;
                    obj.usetime = DateTime.Now;
                    return;
                }
            }

            {
                promotion_coupons_usecase obj = vobj as promotion_coupons_usecase;
                if (obj != null)
                {
                    obj.pkid = 0;
                    obj.coupons_id = 0;
                    obj.case_pamars = string.Empty;
                    obj.case_pamars_value = string.Empty;
                    obj.case_remark = string.Empty;
                    obj.createtime = DateTime.Now;
                    return;
                }
            }
        }
        #endregion
    }
}
