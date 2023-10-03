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
		private readonly IUnitOfWork _unitOfWork;

		public WeatherForecastController(ILogger<WeatherForecastController> logger,
			IUnitOfWork unitOfWork
			)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public async Task<IEnumerable<WeatherForecast>> Get()
		{
			var check = await _unitOfWork.Repository<Product>().ListAllAsync();
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