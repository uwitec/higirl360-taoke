using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiGirl360.Models.Repository;

namespace HiGirl360.Controllers
{
    public class ADController : Controller
    {
        private IAdRepository _repository;

        public ADController()
        {
            _repository = new ADRepository();
        }

        public ADController(IAdRepository repository)
        {
            _repository = repository;
        }

        public ActionResult GetHomeAD()
        {
            var model = _repository.GetHomeAdList();
            return PartialView("_HomeAd", model);
        }

        //
        // GET: /AD/

        public ActionResult Index()
        {
            return View();
        }

    }
}
