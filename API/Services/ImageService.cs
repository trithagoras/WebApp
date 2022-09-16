using System;
namespace API.Services
{
	public class ImageService : IImageService
	{
        public async Task<byte[]> LoadImage(int id)
		{
			var f = await System.IO.File.ReadAllBytesAsync($"Assets/Images/{id}.jpg");
			return f;
		}
    }
}

