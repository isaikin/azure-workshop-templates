using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.AzureWorkShop.Labs.ViewModels
{
	public class ImageVM
	{
		public Guid Id { get; set; }

		public byte[] Data { get; set; }

		public string MimeType { get; set; }
	}
}