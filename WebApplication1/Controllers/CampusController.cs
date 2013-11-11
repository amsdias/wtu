using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CampusController : Controller
    {
        //
        // GET: /Campus/
        public ActionResult Faculty()
        {
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