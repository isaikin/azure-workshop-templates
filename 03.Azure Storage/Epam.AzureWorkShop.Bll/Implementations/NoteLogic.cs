using System;
using System.Collections.Generic;
using System.Linq;
using Epam.AzureWorkShop.Bll.Interfaces;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Bll.Implementations
{
	public class NoteLogic : INoteLogic
	{
		private readonly IRepository<Note> _notesRepository;
		private readonly IRepository<ImageMetadata> _metadataRepository;
		private readonly ImageLogic _imageLogic;
		
		public NoteLogic(IRepository<Note> notesRepository, ImageLogic imageLogic, IRepository<ImageMetadata> metadataRepository)
		{
			_notesRepository = notesRepository;
			_imageLogic = imageLogic;
			_metadataRepository = metadataRepository;
		}

		public Note Add(Note note, Image image)
		{
			var imageId = _imageLogic.Add(image).Id;

			var currentNote = new Note()
			{
				Text = note.Text,
				ImageId = imageId
			};
			
			currentNote = _notesRepository.Add(currentNote);
			
			_metadataRepository.Add(new ImageMetadata
			{
				ImageId = image.Id,
				MimeType = image.MimeType,
				FileName = image.FileName,
				NoteId = currentNote.Id
			});

			return currentNote;
		}

		public void Delete(Guid id)
		{
			var idImage = _notesRepository.GetById(id)?.ImageId;

			if (idImage != null)
			{
				_imageLogic.Delete(idImage.Value);
			}
		}

		public IEnumerable<Note> GetAll()
		{
			var metadata = _metadataRepository.GetAll().ToDictionary(key => key.NoteId);
			var notes = _notesRepository.GetAll();
			foreach (var note in notes)
			{
				note.ImageId = metadata[note.Id].ImageId;
				yield return note;
			}
		}

		public Note GetById(Guid id)
		{ 
			return _notesRepository.GetById(id);
		}
	}
}