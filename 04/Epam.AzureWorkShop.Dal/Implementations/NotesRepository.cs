using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;
using MongoDB.Driver;

namespace Epam.AzureWorkShop.Dal.Implementations
{
	public class NotesRepository : IRepository<Note>
	{
		private readonly IMongoCollection<Note> _notes;


		public NotesRepository()
		{
			var client = new MongoClient(ConfigurationManager.ConnectionStrings["CosmosDatabase"].ConnectionString);
			var database = client.GetDatabase(ConfigurationManager.AppSettings["CosmosDatabaseName"]);

			_notes = database.GetCollection<Note>(ConfigurationManager.AppSettings["CosmosNotesCollectionName"]);
		}

		public Note Add(Note item)
		{
			item.Id = Guid.NewGuid();

			_notes.InsertOne(item);
			return item;
		}

		public IEnumerable<Note> GetAll()
		{
			var result = _notes.FindSync(FilterDefinition<Note>.Empty);
			return result.ToList();
		}

		public void Delete(Guid id)
		{
			_notes.FindOneAndDelete(note => note.Id == id);
		}

		public Note GetById(Guid id)
		{
			var result = _notes.Find(note => note.Id.Equals(id));

			return result.FirstOrDefault();
		}
	}
}