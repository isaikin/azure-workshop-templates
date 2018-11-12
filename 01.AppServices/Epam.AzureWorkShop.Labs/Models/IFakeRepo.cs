using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Labs.Models
{
	public interface IFakeRepo<T> where T : BasicItem
	{
		Guid Add(T item);
		IEnumerable<T> GetAll();
		bool Delete(Guid id);
		T GetById(Guid id);
	}
}