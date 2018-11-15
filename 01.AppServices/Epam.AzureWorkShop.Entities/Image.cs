using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.AzureWorkShop.Entities
{
	public class Image : BasicItem
	{
		public byte[] Data { get; set; }

		public string MimeType { get; set; }

		public string FileName { get; set; }
	}
}
