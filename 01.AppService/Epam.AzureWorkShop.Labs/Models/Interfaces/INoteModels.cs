using System;
using System.Collections.Generic;
using Epam.AzureWorkShop.Labs.ViewModels;

namespace Epam.AzureWorkShop.Labs.Models.Interfaces
{
	public interface INoteModels
	{
		Guid Add(NoteCreateVM note);
		bool Delete(Guid id);
		IEnumerable<NoteVM> GetAll();
		NoteVM GetById(Guid id);
	}
}