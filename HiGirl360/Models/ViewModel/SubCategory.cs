using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.ViewModel
{
    public class SubCategory:Category
    {
        public int ParentCateID { get; set; }
    }
}