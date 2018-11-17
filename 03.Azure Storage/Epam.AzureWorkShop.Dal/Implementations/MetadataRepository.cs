using System;
using System.Collections.Generic;
using System.Linq;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Dal.Implementations
{
    public class MetadataRepository : IRepository<ImageMetadata>
    {
        public ImageMetadata Add(ImageMetadata item)
        {
            using (var context = new SqlContext())
            {
                context.Metadata.Add(item);
                context.SaveChanges();
            }

            return item;
        }

        public IEnumerable<ImageMetadata> GetAll()
        {
            using (var context = new SqlContext())
            {
                return context.Metadata.ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SqlContext())
            {
                var imageMetadata = new ImageMetadata
                {
                    ImageId = id,
                };

                context.Metadata.Attach(imageMetadata);
                context.Metadata.Remove(imageMetadata);

                context.SaveChanges();
            }
        }

        public ImageMetadata GetById(Guid id)
        {
            using (var context = new SqlContext())
            {
                var imageMetadata = context.Metadata.FirstOrDefault(u => u.ImageId == id);

                return imageMetadata;
            }
        }
    }
}