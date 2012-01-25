using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiveThreeOne.Web.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

			ViewBag.Message = "Connection: ";

			return View();
		}

		public ActionResult About() {
			ViewBag.Message = "Your quintessential app description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your quintessential contact page.";

			return View();
		}
	}
}


/*
 * Create excersice
 * Update excerise
 * 
 * Get sets for given excerice / week
 * 
 * Enter result for given excersice / week


*/