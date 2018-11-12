using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.AzureWorkShop.Labs.ViewModels
{
	public class NoteCreateVM
	{
		public string Text { get; set; }

		public byte[] ImageData { get; set; }

		public string MimeTypeImage { get; set; }
	}
}