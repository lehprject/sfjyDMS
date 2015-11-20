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
                    obj.pkid =0;
                    obj.drid = string.Empty;
                    obj.app_time = DateTime.Now;
                    obj.drawmoney = 0;
                    obj.in_bank = 0;
                    //obj.in_bank_acc = string.Empty;
                    obj.opuser = string.Empty;
                    obj.optime=DateTime.Now;
                    obj.opstatus=0;
                    obj.opremark=string.Empty;
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
        }
        #endregion
    }
}
