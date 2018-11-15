using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epam.AzureWorkShop.Labs.ViewModels
{
	[BindAttribute(Exclude = "ImageData")]
	public class NoteCreateVM
	{
		[Required]
		public string Text { get; set; }

		[Display(Name ="Select image")]
		public byte[] ImageData { get; set; }

		public string MimeTypeImage { get; set; }
	}
}