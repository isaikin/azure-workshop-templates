using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Bll.Interfaces
{
	public interface IImageLogic
	{
		Image Add(Image image);
		void Delete(Guid id);
		IEnumerable<Image> GetAll();
		Image GetById(Guid id);
	    Image GetByGetThumbnailByIdId(Guid id);
	}
}