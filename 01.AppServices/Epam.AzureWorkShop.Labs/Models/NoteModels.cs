using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.AzureWorkShop.Entities;
using Epam.AzureWorkShop.Labs.Models.Interfaces;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Models
{
	public class NoteModels : INoteModels
	{
		private readonly IRepository<Note> _notes;
		private readonly IRepository<Image> _images;

		public NoteModels(IRepository<Note> notes, IRepository<Image> images)
		{
			_notes = notes;
			_images = images;
		}

		public Guid Add(NoteCreateVM note)
		{
			var currentImage = new Image()
			{
				Data =  note.ImageData,
				MimeType =  note.MimeTypeImage,
			};

			var idImage = _images.Add(currentImage);

			var currentNote = new Note()
			{
				Text = note.Text,
				ImageId = idImage,
			};

			return _notes.Add(currentNote);
		}

		public bool Delete(Guid id)
		{
			var idImage = _notes.GetById(id)?.ImageId;

			if (idImage != null)
			{
				_images.Delete(idImage.Value);
			}

			return _notes.Delete(id);
		}

		public IEnumerable<NoteVM> GetAll()
		{
			return _notes.GetAll().Select(item => new NoteVM()
			{
				Id =  item.Id,
				Text =  item.Text,
				ImageId =  item.ImageId,
			});
		}

		public NoteVM GetById(Guid id)
		{
			var item = _notes.GetById(id);
			return new NoteVM() 
			{
				Id = item.Id,
				Text = item.Text,
				ImageId = item.ImageId,
			};
		}
	}
}