using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiGirl360.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index(string keyword,string type)
        {
            ViewBag.SearchKey = keyword;
            return View();
        }

    }
}
