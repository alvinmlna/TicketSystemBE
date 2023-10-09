using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface ITicketServices
	{
		Task<bool> AddTicket(Ticket ticket, List<Attachment> attachments);

		Task<bool> Edit(EditTicketRequest ticket);


		Task<bool> UploadFile(List<Attachment> attachments, int ticketId);

		Task<Ticket> GetTicketById(int id);

		Task<List<ListTicketResponse>> ListTicketResponse(ListTicketRequest listTicketRequest);
	}
}
