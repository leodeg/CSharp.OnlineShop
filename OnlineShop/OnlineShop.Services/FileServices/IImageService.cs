using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Services.FileServices
{
	public interface IImageService
	{
		Task<string> SaveImage(IFormFile image);
		void DeleteImage(string imageName);
	}
}