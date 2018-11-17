using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Epam.AzureWorkShop.Labs.Models;
using Epam.AzureWorkShop.Labs.Models.Interfaces;

namespace Epam.AzureWorkShop.Labs.Controllers
{
	public class ImageController : Controller
	{
		private readonly IImageModels _imageModels;

		public ImageController(IImageModels imageModels)
		{
			_imageModels = imageModels;
		}

		public ActionResult GetById(Guid id)
		{
			var image = _imageModels.GetById(id);

			Response.StatusCode = (int)HttpStatusCode.NotFound;
			return image == null
				? null
				: new FileContentResult(image.Data, image.MimeType);
		}
	}
}
