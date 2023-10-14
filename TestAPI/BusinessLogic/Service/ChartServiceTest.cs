using BusinessLogic.Services;
using Core.Interfaces.Services;
using DataAccess.Repository;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic.Service
{
	[TestClass]
	public class ChartServiceTest
	{
		static UnitOfWork _unitOfWork = UnitOfWorkHelpers.GetInMemories();
		private readonly IChartService _chartService = new ChartService(_unitOfWork);


		[TestMethod]
		public async Task GetLast12MonthTickets()
		{
			var result = await _chartService.GetLast12MonthTickets();
		}
	}

}
