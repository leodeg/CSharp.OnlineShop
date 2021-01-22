using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Services.FileServices
{
	public class ImageService : IImageService
	{
		private string rootPath;
		private readonly ImageManager imageManager;

		public ImageService(string rootPath, ImageManager imageManager)
		{
			this.rootPath = rootPath;
			this.imageManager = imageManager;
		}

		public async Task<string> SaveImage(IFormFile image)
		{
			return await imageManager.SaveImage(image, rootPath);
		}

		public void DeleteImage(string imageName)
		{
			imageManager.DeleteImage(imageName, rootPath);
		}
	}
}
