using System;

namespace Epam.AzureWorkShop.Entities
{
    public class ImageMetadata
    {
        public Guid ImageId { get; set; }

        public Guid ThumbnailId { get; set; }

        public Guid UserId { get; set; }
        
        public string MimeType { get; set; }

        public string Name { get; set; }
    }
}