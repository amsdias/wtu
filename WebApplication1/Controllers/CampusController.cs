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
        public ActionResult Accommodation()
        {
            return View();
        }
        public ActionResult Mobility()
        {
            return View();
        }
	}
}