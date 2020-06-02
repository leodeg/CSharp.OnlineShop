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
	public class ImageManager
	{
		/// <summary>
		/// Save image to root path with unique name and return the file name.
		/// </summary>
		public async Task<string> SaveImage(IFormFile image, string rootPath)
		{
			string savePath = Path.Combine(rootPath);
			if (!Directory.Exists(savePath))
				Directory.CreateDirectory(savePath);

			string uniqueName = Guid.NewGuid().ToString();
			string imageType = image.FileName.Substring(image.FileName.LastIndexOf("."));
			string fileName = $"img_{uniqueName}{imageType}";

			using (FileStream fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}

			return fileName;
		}

		/// <summary>
		/// Delete image from root path.
		/// </summary>
		/// <exception cref="FileNotFoundException" />
		public void DeleteImage(string imageName, string rootPath)
		{
			string deletePath = Path.Combine(rootPath);
			if (!Directory.Exists(deletePath))
				throw new FileNotFoundException();

			try
			{
				string filePath = Path.Combine(deletePath, imageName);
				if (File.Exists(filePath))
					File.Delete(filePath);
			}
			catch (DirectoryNotFoundException ex)
			{
				// TODO: logging errors
				Trace.WriteLine(ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Check and replace old image if exist, or just save new image.
		/// </summary>
		/// <exception cref="ArgumentException" />
		public async Task<string> SaveOrCreateImage(string rootPath, string oldImagePath, string imagePath, IFormFile image)
		{
			if (string.IsNullOrWhiteSpace(oldImagePath))
				return await SaveImage(image, rootPath);

			if (oldImagePath != imagePath)
			{
				DeleteImage(oldImagePath, rootPath);
				return await SaveImage(image, rootPath);
			}

			throw new ArgumentException();
		}
	}
}
