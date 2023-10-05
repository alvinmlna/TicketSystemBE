using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Serilog;

namespace BusinessLogic.Services
{
	public partial class TicketService : ITicketServices
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

		public async Task<Ticket> GetTicketById(int id)
		{
			return await _unitOfWork.TicketRepository.GetTicketById(id);
		}

	}
}
