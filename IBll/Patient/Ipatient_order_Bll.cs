﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ConfigClass;

namespace IBll
{
    public interface Ipatient_order_Bll
    {
        List<patient_order> SearchPatientOrderList(int patient_id, int drid, int service_type, DateTime? createtime, DateTime? createtime2,
       orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
    }
}
