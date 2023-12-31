﻿using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface ITicketServices
	{
		Task<Ticket?> AddTicket(AddTicketRequest request);

		Task<bool> Edit(EditTicketRequest ticket);

		Task<bool> UploadFile(List<Attachment> attachments, int ticketId);

		Task<Ticket> GetTicketById(int id);

		Task<DefaultResponse> LockTicketRow(Ticket ticket, int UserId);

		Task<DefaultResponse> UnlockTicketByUser(int UserId);

        Task<List<ListTicketResponse>> ListTicketResponse(ListTicketRequest listTicketRequest);
    }
}
