using Core.DTO.Response;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class ChartService : IChartService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ChartService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public Task<CategoryChartResponse> GetCategoryChart(string type)
		{
			throw new NotImplementedException();
		}

		public Task<Last12MonthTicketsReponse> GetLast12MonthTickets()
		{
			throw new NotImplementedException();
		}

		public async Task<List<StatusSummaryResponse>> GetStatusSummaryResponses()
		{
			return await _unitOfWork.TicketRepository.GetStatusSummary();
		}
	}
}
