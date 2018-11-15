using System;
using System.Collections.Generic;
using System.Linq;
using Epam.AzureWorkShop.Bll.Interfaces;
using Epam.AzureWorkShop.Dal;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Bll.Implementations
{
	public class ImageLogic : IImageLogic
	{
		private readonly IRepository<Image> _imageRepository;
		private readonly IRepository<ImageMetadata> _metadataRepository;

		public ImageLogic(IRepository<Image> imageRepository, IRepository<ImageMetadata> metadataRepository)
		{
			_imageRepository = imageRepository;
			_metadataRepository = metadataRepository;
		}

		public Image Add(Image image)
		{
			image = _imageRepository.Add(image);
			
			return image;
		}

		public void Delete(Guid id)
		{
			_imageRepository.Delete(id);
		}

		public IEnumerable<Image> GetAll()
		{
			var metadata = _metadataRepository.GetAll().ToDictionary(key => key.ImageId);
			
			var images = _imageRepository.GetAll().ToList();
			foreach (var image in images)
			{
				image.MimeType = metadata[image.Id].MimeType;
			}
			
			return images;
		}

		public Image GetById(Guid id)
		{
			var item = _imageRepository.GetById(id);
			var metadata = _metadataRepository.GetById(id);
			item.MimeType = metadata.MimeType;
			
			return item;
		}
	}
}