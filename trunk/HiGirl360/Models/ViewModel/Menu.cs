using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.ViewModel
{
    public class Menu
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }

        public List<SubMenu> SubMenu { get; set; }
    }
}