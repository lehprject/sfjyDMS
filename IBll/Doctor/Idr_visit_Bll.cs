using System;
namespace IBll
{
    public interface Idr_visit_Bll
    {
        Model.dr_pre_visit CreatePreVisit(Model.dr_pre_visit info, out string error);
        Model.dr_visit CreateVisit(Model.dr_visit info, out string error);
        System.Collections.Generic.List<Model.dr_pre_visit> SearchPreVisitList(int visit_id, int patient_id, DateTime? pre_date1, DateTime? pre_date2, string pre_time, int pre_type, DateTime? createtime1, DateTime? createtime2, Model.ConfigClass.orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
        System.Collections.Generic.List<Model.dr_visit> SearchVisitList(int hospital_id, int drid, DateTime? visit_date1, DateTime? visit_date2, string visit_time, int service_type, int status, DateTime? createtime1, DateTime? createtime2, Model.ConfigClass.orderbyEnum? orderby, string orderbyCol, int pageIndex, int pageSize, out string error);
    }
}
