using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Serilog;

namespace BusinessLogic.Services
{
	public class TicketService : ITicketServices
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILoggingService _log;

		public TicketService(IUnitOfWork unitOfWork, ILoggingService log)
        {
			_unitOfWork = unitOfWork;
			_log = log;
		}

        public async Task<bool> AddTicket(Ticket ticket, List<Attachment> attachments)
		{
			ticket.RaisedDate = DateTime.Now;

			int SLA = await GetSLA(ticket.PriorityId);
			ticket.ExpectedDate = DateTime.Now.AddMinutes(SLA);

			foreach (var attachment in attachments)
			{
				attachment.DateAdded = DateTime.Now;
				ticket.Attachments.Add(attachment);
			}


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
