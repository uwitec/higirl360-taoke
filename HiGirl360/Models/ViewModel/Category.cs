using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.ViewModel
{
    public class Category
    {
        public int CateID { get; set; }
        public string CateName { get; set; }
        public string CateDesc { get; set; }

        public string Keywords { get; set; }
        public string Description { get; set; }

        public List<SubCategory> SubCategory { get; set; }
    }
}