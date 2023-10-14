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

		public async Task<Last12MonthTicketsReponse> GetLast12MonthTickets()
		{
			var fromDB = await _unitOfWork.TicketRepository.GetLast12MonthTickets();

			Last12MonthTicketsReponse result = new Last12MonthTicketsReponse()
			{
				Month = fromDB.Select(x => x.CreatedDate.Month.ToString()).ToArray(),
				MonthName = fromDB.Select(x => x.CreatedDate.ToString("MMM")).ToArray(),
				Count = fromDB.Select(x => x.Count.ToString()).ToArray()
			};

			return result;
		}

		public async Task<List<StatusSummaryResponse>> GetStatusSummaryResponses()
		{
			return await _unitOfWork.TicketRepository.GetStatusSummary();
		}
	}
}
