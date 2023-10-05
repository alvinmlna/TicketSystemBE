using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface ITicketServices
	{
		Task<bool> AddTicket(Ticket ticket, List<Attachment> attachments);

		Task<Ticket> GetTicketById(int id);
	}
}
