using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Bll.Interfaces
{
	public interface INoteLogic
	{
		Note Add(Note note, Image image);
		void Delete(Guid id);
		IEnumerable<NoteViewItem> GetAll();
		NoteViewItem GetById(Guid id);
	}
}