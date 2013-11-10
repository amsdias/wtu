using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Ualg";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            return View();
        }
        public ActionResult Mobility()
        {
            ViewBag.Message = "Mobility Office";

            return View();
        }
        public ActionResult Accommodation()
        {
            ViewBag.Message = "Accommodation";

            return View();
        }
    }
}