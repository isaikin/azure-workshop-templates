using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Epam.AzureWorkShop.Labs.Models;
using Epam.AzureWorkShop.Labs.Models.Interfaces;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Controllers
{
	public class HomeController : Controller
	{
		private readonly INoteModels _noteModels;

		public HomeController(INoteModels noteModels)
		{
			_noteModels = noteModels;
		}

		public ActionResult Index()
		{
			ViewBag.Notes = _noteModels.GetAll();
			
			 return View();
		}
		
		[HttpPost]
		public ActionResult AddPost(NoteCreateVM note)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Notes = _noteModels.GetAll();
				
				return View("Index", note);
			}

			ReadImage(note);
			_noteModels.Add(note);
			return RedirectToAction("index");
		}

		private void ReadImage(NoteCreateVM note)
		{
			if (Request.Files["ImageData"] != null)
			{
				using (var memory = new BinaryReader(Request.Files["ImageData"].InputStream))
				{
					var data = memory.ReadBytes((int) Request.Files["ImageData"].InputStream.Length);

					note.ImageData = data;
					note.MimeTypeImage = Request.ContentType;
				}
			}
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}