using Epam.AzureWorkShop.Entities;
using System;
using System.Collections.Generic;

namespace Epam.AzureWorkShop.Dal.Interfaces
{
	public interface IMetaDataRepository
	{
		ImageMetadata Add(ImageMetadata item);

		IEnumerable<ImageMetadata> GetAll();

		void Delete(Guid id);

		ImageMetadata GetByNoteId(Guid id);

		ImageMetadata GetByImageId(Guid id);
	}
}