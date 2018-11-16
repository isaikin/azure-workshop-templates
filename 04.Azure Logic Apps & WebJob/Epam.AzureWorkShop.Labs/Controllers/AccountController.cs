using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Epam.AzureWorkShop.Bll.Implementations;
using Epam.AzureWorkShop.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Epam.AzureWorkShop.Labs.Models;

namespace Epam.AzureWorkShop.Labs.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IAppUserLogic _appUserLogic;

		public AccountController(IAppUserLogic appUserLogic)
		{
			_appUserLogic = appUserLogic;
		}

		//
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = _appUserLogic.IsAuthenticate(new UserCredentials()
			{
				Username = model.Email,
				Password = model.Password,
			});

			if (result)
			{
				FormsAuthentication.SetAuthCookie(model.Email, true);

				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Invalid login attempt.");
			return View(model);
		}

		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				_appUserLogic.Add(new UserCredentials()
				{
					Username = model.Email,
					Password = model.Password,
				});
				
				FormsAuthentication.SetAuthCookie(model.Email, true);
				
				return RedirectToAction("Index", "Home");
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

		
	
	}
}