using Core.DTO.Request;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Serilog;
using System.Net.Sockets;

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
			ticket.ExpectedDate = DateTime.Now.AddHours(SLA);

			_unitOfWork.Repository<Ticket>().Add(ticket);
			int result = await _unitOfWork.SaveChanges();
			if (result > 0)
				return true;

			return false;
		}

		public async Task<bool> Edit(EditTicketRequest ticket)
		{
			var toUpdate = await _unitOfWork.Repository<Ticket>().GetByIdAsync(ticket.TicketId);
			if (toUpdate == null) { return false; }

			toUpdate.AssignedToId = ticket.AssignedToId;
			toUpdate.ProductId = ticket.ProductId;
			toUpdate.CategoryId = ticket.CategoryId;
			toUpdate.PriorityId = ticket.PriorityId;
			toUpdate.StatusId = ticket.StatusId;

			_unitOfWork.Repository<Ticket>().Update(toUpdate);
			return await _unitOfWork.SaveChangesReturnBool();
		}

		public async Task<Ticket> GetTicketById(int id)
		{
			return await _unitOfWork.TicketRepository.GetTicketById(id);
		}

		public async Task<bool> UploadFile(List<Attachment> attachments, int? ticketId)
		{
			var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync((int)ticketId);
			if (ticket == null) { return false; }


			foreach (var attachment in attachments)
			{
				attachment.DateAdded = DateTime.Now;
				ticket.Attachments.Add(attachment);
			}

			_unitOfWork.Repository<Ticket>().Update(ticket);
			return await _unitOfWork.SaveChangesReturnBool();
		}
	}
}
