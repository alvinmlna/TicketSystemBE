using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class TicketServices : ITicketServices
	{
		private readonly IUnitOfWork _unitOfWork;

		public TicketServices(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<bool> AddTicket(Ticket ticket)
		{
			ticket.RaisedDate = DateTime.Now;

			int SLA = await GetSLA(ticket.PriorityId);
			ticket.ExpectedDate = DateTime.Now.AddMinutes(SLA);

			_unitOfWork.Repository<Ticket>().Add(ticket);

			int result = await _unitOfWork.SaveChanges();
			if (result > 0)
				return true;

			return false;
		}


		private async Task<int> GetSLA(int priorityId)
		{
			var result = await _unitOfWork.Repository<Priority>().GetByIdAsync(priorityId);
			if (result != null) return result.ExpectedLimit;

			return 0;
		}
	}
}
