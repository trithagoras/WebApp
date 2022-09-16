using System;
namespace API.Services
{
	public interface IImageService
	{
		public Task<byte[]> LoadImage(int id);
	}
}

