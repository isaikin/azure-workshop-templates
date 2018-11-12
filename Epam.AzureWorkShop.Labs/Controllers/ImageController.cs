using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Epam.AzureWorkShop.Labs.Models;

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


		    return image == null
			    ? Response()
			    : (IHttpActionResult) new FileContentResult(image.Data, image.MimeType);

	    }
    }
}
