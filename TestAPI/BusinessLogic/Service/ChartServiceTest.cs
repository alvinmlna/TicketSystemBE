using BusinessLogic.Services;
using Core.Interfaces.Services;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic.Service
{
	[TestClass]
	public class ChartServiceTest
	{
		static TicketDBContext _dbContext = UnitOfWorkHelpers.GetInMemoriesDBContext();
        static UnitOfWork _unitOfWork = new UnitOfWork(_dbContext,
            new ConfigurationRepository(_dbContext),
            new TicketRepository(_dbContext),
            new DiscussionRepository(_dbContext),
            new UserRepository(_dbContext));

        private readonly IChartService _chartService = new ChartService(_unitOfWork);


		[TestMethod]
		public async Task GetLast12MonthTickets()
		{
			var result = await _chartService.GetLast12MonthTickets();
		}
	}

}
