using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Entities;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Models.Interfaces
{
	public interface INoteModels
	{
		Note Add(NoteCreateVM note);
		void Delete(Guid id);
		IEnumerable<NoteVM> GetAll();
		NoteVM GetById(Guid id);
	}
}