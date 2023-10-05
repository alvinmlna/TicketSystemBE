using API.Helpers;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class FileController : BaseApiController
	{
		[HttpGet("{filename}")]
		public ActionResult Generate(string filename)
		{
			string filePath = Path.Combine(AppContext.BaseDirectory + "images", filename);
			if (!System.IO.File.Exists(filePath)) return ApiResponseHelpers.NotFound(filename);
			
			
			var streamResult = new FileStream(filePath, FileMode.Open);
			return File(streamResult, "application/octet-stream", filename);
		}
	}
}
