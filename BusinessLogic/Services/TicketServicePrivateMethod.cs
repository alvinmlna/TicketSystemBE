
using Core.Entities;

namespace BusinessLogic.Services
{
	public partial class TicketService
	{
		private async Task<int> GetSLA(int priorityId)
		{
			var result = await _unitOfWork.Repository<Priority>().GetByIdAsync(priorityId);
			if (result != null) return result.ExpectedLimit;

			return 0;
		}
	}
}
