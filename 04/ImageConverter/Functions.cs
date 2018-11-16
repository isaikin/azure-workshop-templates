using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using Epam.AzureWorkShop.Dal.Implementations;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace ImageConverter
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([ServiceBusTrigger("thumbnails")] BrokeredMessage message, TextWriter log)
        {
            var imgOriginalId = (Guid)message.Properties["imageId"];
            //var imgOriginalId = Newtonsoft.Json.JsonConvert.DeserializeObject<Guid>(b);

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
