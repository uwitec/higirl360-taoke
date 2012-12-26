using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiGirl360.Models.Repository;

namespace HiGirl360.Controllers
{
    public class CategoryController : Controller
    {
        //显示所有分类列表可是部分分类

        private CategoryRepository _repository;

        public CategoryController()
        {
            _repository = new CategoryRepository();
        }

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TopCategory()
        {
            var model = _repository.GetTopCategory();
            return PartialView("_TopCategory", model);
        }
    }
}
