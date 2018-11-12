using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Labs.Models
{
	public class FakeRepo<T> : IFakeRepo<T> where T: BasicItem
	{
		private readonly Dictionary<Guid, T> _data;

		public FakeRepo()
		{
			_data = new Dictionary<Guid, T>();	
		}

		public Guid Add(T item)
		{
			var id = Guid.NewGuid();
			item.Id = id;
			_data.Add(id, item);

			return id;
		}

		public IEnumerable<T> GetAll()
		{
			return _data.Values;
		}

		public bool Delete(Guid id)
		{
			if (!_data.ContainsKey(id))
			{
				throw new ArgumentException($"id {id} is not found");
			}

			return _data.Remove(id);
		}

		public T GetById(Guid id)
		{
			if (!_data.TryGetValue(id, out var value))
			{
				return null;
			}

			return value;
		}
	}
}