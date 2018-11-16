using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.AzureWorkShop.Entities
{
	public class NoteViewItem : Note
	{
		public Guid ThumbnailId { get; set; }
	}
}
