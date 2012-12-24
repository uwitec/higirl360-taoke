using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.ViewModel
{
    public class Menu
    {
        public int CateID { get; set; }
        public string CateName { get; set; }

        //private List<SubMenu> _subMenu;
        public List<SubMenu> SubMenu { get; set; }
    }
}