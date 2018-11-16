﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.AzureWorkShop.Dal.Implementations;
using Epam.AzureWorkShop.Dal.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ConvectorImages
{
	public class Functions
	{
		public static void ProcessQueueMessage([ServiceBusTrigger("queueerrorsandwarnings")] BrokeredMessage message, TextWriter log)
		{
			using (var queue = new ServiceBusQueue())
			{
				var imgOriginalId = queue.Get<Guid>();
				IRepository<Epam.AzureWorkShop.Entities.Image> imgRepo = new ImageRepository();
				var currentImg = imgRepo.GetById(imgOriginalId);

				using (var image = Image.Load(currentImg.Data))
				{
					image.Mutate(i => i.Resize(150, 150));
					using (var outputMemory = new MemoryStream())
					{
						image.Save(outputMemory, new PngEncoder());

						var id = imgRepo.Add(new Epam.AzureWorkShop.Entities.Image
						{
							Data = outputMemory.ToArray(),
						}).Id;

						var metRep = new MetadataRepository();
						var metData = metRep.GetByImageId(imgOriginalId);
						metData.ThumbnailId = id;
						metRep.Update(metData);
					}
				}
			}
		}
	}
}
