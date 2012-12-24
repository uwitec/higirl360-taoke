using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiGirl360.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult JoinUs()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult JoinUs(FormCollection from)
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string username, string password)
        {
            return View();
        }

        public void LogOff()
        {
            RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(string password, string newpassword)
        {
            return View();
        }

        [Authorize]
        public ActionResult AddMoreInfo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMoreInfo(FormContext from)
        {
            return View();
        }

    }
}
