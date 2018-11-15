using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Entities;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Models.Interfaces
{
	public interface IImageModels
	{
		Image Add(ImageCreateVM image);
		void Delete(Guid id);
		IEnumerable<ImageVM> GetAll();
		ImageVM GetById(Guid id);
	}
}