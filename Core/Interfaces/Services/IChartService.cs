using Core.DTO.Response;

namespace Core.Interfaces.Services
{
    public interface IChartService
	{
		Task<List<StatusSummaryResponse>> GetStatusSummaryResponses();

		Task<Last12MonthTicketsReponse> GetLast12MonthTickets();

		Task<CategoryChartResponse> GetCategoryChart(string type);
	}
}
