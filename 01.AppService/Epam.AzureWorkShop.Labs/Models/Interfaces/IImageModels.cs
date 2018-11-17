using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Models.Interfaces
{
	public interface IImageModels
	{
		Guid Add(ImageCreateVM image);
		bool Delete(Guid id);
		IEnumerable<ImageVM> GetAll();
		ImageVM GetById(Guid id);
	}
}