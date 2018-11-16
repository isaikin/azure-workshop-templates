using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Epam.AzureWorkShop.Dal.Implementations
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly string _blobContainerName;
        
        public ImageRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BlobStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            _cloudBlobClient = storageAccount.CreateCloudBlobClient();
            _blobContainerName = ConfigurationManager.AppSettings["BlobContainerName"];
        }
        
        public Image Add(Image item)
        { 
            item.Id = Guid.NewGuid();
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_blobContainerName);
            // cloudBlobContainer.Create();
 
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            cloudBlobContainer.SetPermissions(permissions);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(item.Id.ToString());
            cloudBlockBlob.BeginUploadFromByteArray(item.Data,0, item.Data.Length, null, null);

            return item;
        }

        public IEnumerable<Image> GetAll()
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_blobContainerName);
            var blobs = cloudBlobContainer.ListBlobs().OfType<CloudBlockBlob>();

            foreach (var blob in blobs)
            {
                var image = new Image
                {
                    Id = Guid.Parse(blob.Name)
                };
                blob.DownloadToByteArray(image.Data, 0);

                yield return image;
            }

        }

        public void Delete(Guid id)
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_blobContainerName);
            var blob = cloudBlobContainer.GetBlobReference(id.ToString());
            
            blob.Delete();
        }

        public Image GetById(Guid id)
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_blobContainerName);
            var blob = cloudBlobContainer.GetBlobReference(id.ToString());

            var image = new Image
            {
                Id = id
            };

            using (var ms = new MemoryStream())
            {
                blob.DownloadToStream(ms);
                image.Data = ms.ToArray();
            }
            
            return image;

        }
    }
}