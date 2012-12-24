using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiGirl360.Models.Repository;

namespace HiGirl360.Controllers
{
    public class MenuController : Controller
    {
        private MenuRepository _menuRepository;

        public MenuController()
        {
            _menuRepository = new MenuRepository();
        }
        //
        // GET: /Menu/

        [ChildActionOnly]
        public ActionResult Index(string viewName)
        {
            var model = _menuRepository.GetMenu();
            return PartialView(viewName, model);
        }

    }
}
