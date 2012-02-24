//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using FiveThreeOne.Service;
//using FiveThreeOne.Web.Models;

//namespace FiveThreeOne.Web.Controllers {

//	[Authorize]
//	public class AccountController : Controller {
//		private readonly IIdentification identification;
		
//		public AccountController(IIdentification identification) {
//			this.identification = identification;
//		}

//		//
//		// GET: /Account/LogOn

//		[AllowAnonymous]
//		public ActionResult LogOn() {
//			return View();
//		}

//		//
//		// POST: /Account/LogOn

//		[AllowAnonymous]
//		[HttpPost]
//		public ActionResult LogOn(LogOnModel model, string returnUrl) {
//			if (ModelState.IsValid) {
//				identification.Authenticate(model.UserName);

//				FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
//				if (Url.IsLocalUrl(returnUrl)) {
//					return Redirect(returnUrl);
//				}
//				else {
//					return RedirectToAction("Index", "Home");
//				}
//			}
//			else {
//				ModelState.AddModelError("", "The user name or password provided is incorrect.");
//			}

//			return View(model);
//		}

//		public ActionResult LogOff() {
//			FormsAuthentication.SignOut();
//			return RedirectToAction("Index", "Home");
//		}

//	}
//}
