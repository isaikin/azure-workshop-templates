using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.AzureWorkShop.Entities
{
    public class Note : BasicItem
	{
	    public string Text { get; set; }

	    public Guid ImageId { get; set; }

	}
}
