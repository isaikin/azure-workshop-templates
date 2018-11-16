using System;
using System.Web.Mvc;
using Epam.AzureWorkShop.Bll.Interfaces;

namespace Epam.AzureWorkShop.Labs.Controllers
{
	public class ImageController : Controller
	{
		private readonly IImageLogic _imageLogic;

		public ImageController(IImageLogic imageLogic)
		{
			_imageLogic = imageLogic;
		}

		public ActionResult GetById(Guid id)
		{
			var image = _imageLogic.GetById(id);
			return image == null ? null : new FileContentResult(image.Data, image.MimeType);
		}
	}
}
