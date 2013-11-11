using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UsefulController : Controller
    {
        //
        // GET: /Useful/
        public ActionResult Bus()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            ViewBag.Message = "Contact us";

            return View();
        }
	}
}