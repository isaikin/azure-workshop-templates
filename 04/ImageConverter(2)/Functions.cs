using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Epam.AzureWorkShop.Dal.Implementations;
using Epam.AzureWorkShop.Dal.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace ImageConverter_2_
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

                using (var image = MediaTypeNames.Image.Load(currentImg.Data))
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

                        using (var httpClient = new HttpClient())
                        {
                            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["LogicAppUri"]);

                            var content = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>(nameof(ImageMetadata.FileName), metData.FileName),
                                new KeyValuePair<string, string>(nameof(ImageMetadata.ImageId), metData.ImageId.ToString()),
                                new KeyValuePair<string, string>(nameof(ImageMetadata.ThumbnailId), metData.ThumbnailId.ToString()),
                            });
                            httpClient.PostAsync(ConfigurationManager.AppSettings["LogicAppUri"], content).Wait();
                        }
                    }
                }
            }
        }
    }
}
