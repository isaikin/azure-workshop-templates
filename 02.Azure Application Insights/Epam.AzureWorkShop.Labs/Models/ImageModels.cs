using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.AzureWorkShop.Entities;
using Epam.AzureWorkShop.Labs.Models.Interfaces;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Models
{
	public class ImageModels : IImageModels
	{
		private readonly IRepository<Image> _images;

		public ImageModels(IRepository<Image> images)
		{
			_images = images;
		}

		public Guid Add(ImageCreateVM image)
		{
			var currentImage = new Image()
			{
				Data = image.Data,
				MimeType = image.MimeType,
			};

			return _images.Add(currentImage);
		}

		public bool Delete(Guid id)
		{
			return _images.Delete(id);
		}

		public IEnumerable<ImageVM> GetAll()
		{
			return _images.GetAll().Select(item => new ImageVM()
			{
				Id = item.Id,
				Data = item.Data,
				MimeType = item.MimeType,
			});
		}

		public ImageVM GetById(Guid id)
		{
			var item = _images.GetById(id);
			return new ImageVM()
			{
				Id = item.Id,
				Data = item.Data,
				MimeType = item.MimeType,
			};
		}
	}
}