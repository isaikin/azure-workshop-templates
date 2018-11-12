using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.AzureWorkShop.Labs.ViewModels
{
	public class NoteVM
	{
		public Guid Id { get; set; }

		public string Text { get; set; }

		public Guid ImageId { get; set; }
	}
}