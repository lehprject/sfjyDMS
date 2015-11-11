using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IBll
{
    public interface Imd_cashdraw_app_Bll
    {
        md_cashdraw_app CreateCashdrawApp(md_cashdraw_app info, out string error);
    }
}
