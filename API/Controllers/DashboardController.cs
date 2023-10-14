using API.Helpers;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class DashboardController : BaseApiController
	{
		private readonly IChartService _chartService;

		public DashboardController(
			IChartService chartService)
        {
			_chartService = chartService;
		}

        [HttpGet("last12month")]
		public async Task<ActionResult> GetLast12MonthTicket()
		{
			var result = await _chartService.GetLast12MonthTickets();
			if (result == null) return ApiResponseHelpers.NotFound("No data");

			return Ok(result);
		}
	}
}
