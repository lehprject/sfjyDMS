using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class vm_BUI_config
    {
        public string id { get; set; }
        public string homePage { get; set; }

        public List<vm_bui_menuContainer> menu { get; set; }
    }

    public class vm_bui_menuContainer
    {
        public string text { get; set; }

        public List<vm_bui_menu> items { get; set; }
    }

    public class vm_bui_menu
    {
        public string id { get; set; }
        public string text { get; set; }
        public string href { get; set; }
    }
}
