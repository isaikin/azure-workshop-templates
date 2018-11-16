using System.IO;
using Microsoft.Azure.WebJobs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;


namespace ThumbnailCreator
{
    public class Functions
    {
        public static void ConvertImage()
        {
			using (var queue = new ServiceBusQueue())
			{

			}

			using (var image = Image.Load(inputStream))
			{
				image.Mutate(i => i.Resize(100, 100));
				image.Save(outputStream, new PngEncoder());
			}
        }
    }
}
