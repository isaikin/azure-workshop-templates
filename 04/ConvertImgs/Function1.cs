using System;
using System.IO;
using Epam.AzureWorkShop.Dal.Implementations;
using Epam.AzureWorkShop.Dal.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ConvertImgs
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
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
