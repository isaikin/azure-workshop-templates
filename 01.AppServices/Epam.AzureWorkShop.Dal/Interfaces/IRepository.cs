using System;
using System.Collections.Generic;

namespace Epam.AzureWorkShop.Dal.Interfaces
{
	public interface IRepository<T>
	{
		T Add(T item);
		
		IEnumerable<T> GetAll();
		
		void Delete(Guid id);
		
		T GetById(Guid id);
	}
}