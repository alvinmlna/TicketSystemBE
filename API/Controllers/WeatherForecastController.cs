using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IGenericRepository<Product> _productRepo;

		public WeatherForecastController(ILogger<WeatherForecastController> logger,
			IGenericRepository<Product> productRepo
			)
		{
			_logger = logger;
			_productRepo = productRepo;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public async Task<IEnumerable<WeatherForecast>> Get()
		{
			var check = await _productRepo.ListAllAsync();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}