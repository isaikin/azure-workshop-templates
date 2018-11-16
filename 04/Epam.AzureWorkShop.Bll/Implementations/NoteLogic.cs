using System;
using System.Collections.Generic;
using System.Linq;
using Epam.AzureWorkShop.Bll.Interfaces;
using Epam.AzureWorkShop.Dal.Implementations;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Bll.Implementations
{
	public class NoteLogic : INoteLogic
	{
		private readonly IRepository<Note> _notesRepository;
		private readonly IMetaDataRepository _metadataRepository;
		private readonly ImageLogic _imageLogic;
		private readonly IServiceBusQueue _serviceBusQueue;

		public NoteLogic(IRepository<Note> notesRepository, ImageLogic imageLogic, IMetaDataRepository metadataRepository, IServiceBusQueue serviceBusQueue)
		{
			_notesRepository = notesRepository;
			_imageLogic = imageLogic;
			_metadataRepository = metadataRepository;
			_serviceBusQueue = serviceBusQueue;
		}

		public Note Add(Note note, Image image)
		{
			var imageId = _imageLogic.Add(image).Id;
			_serviceBusQueue.Add(imageId);

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

		public IEnumerable<NoteViewItem> GetAll()
		{

			var metadata = _metadataRepository.GetAll ().ToDictionary(key => key.NoteId);
			var notes = _notesRepository.GetAll();
			foreach (var note in notes)
			{
				yield return new NoteViewItem
				{
					Id = note.Id,
					ImageId = metadata[note.Id].ImageId,
					Text = note.Text,
					ThumbnailId = metadata[note.Id].ThumbnailId,
				};
			}
		}

		public NoteViewItem GetById(Guid id)
		{ 
			var note = _notesRepository.GetById(id);
			var metadata = _metadataRepository.GetByNoteId(id);

			return new NoteViewItem
			{
				Id = note.Id,
				ImageId = metadata.ImageId,
				Text = note.Text,
				ThumbnailId = metadata.ThumbnailId,
			};
		}
	}
}