using System.IO;
using System.Web.Mvc;
using Epam.AzureWorkShop.Bll.Interfaces;
using Epam.AzureWorkShop.Entities;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Controllers
{
	public class HomeController : Controller
	{
		private readonly INoteLogic _noteLogic;

		public HomeController(INoteLogic noteLogic)
		{
			_noteLogic = noteLogic;
		}

		public ActionResult Index()
		{
			ViewBag.Notes = _noteLogic.GetAll();
			
			 return View();
		}
		
		[HttpPost]
		public ActionResult AddPost(NoteCreateVM noteVm)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Notes = _noteLogic.GetAll();
				
				return View("Index", noteVm);
			}

			var image = ReadImage();
			var note = new Note
			{
				Text = noteVm.Text
			};

			_noteLogic.Add(note, image);
			return RedirectToAction("index");
		}

		private Image ReadImage()
		{
			if (Request.Files["ImageData"] != null)
			{
				using (var memory = new BinaryReader(Request.Files["ImageData"].InputStream))
				{
					var data = memory.ReadBytes((int) Request.Files["ImageData"].InputStream.Length);
					var fileName = Request.Files["ImageData"].FileName;
					return new Image
					{
						Data = data, 
						MimeType = Request.ContentType,
						FileName = fileName
					};
				}
			}

			return null;
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