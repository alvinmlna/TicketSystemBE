﻿using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;

namespace Core.Interfaces.Repository
{
	public interface ITicketRepository : IGenericRepository<Ticket>
	{
		Task<Ticket> GetTicketById(int id);
		Task<List<Ticket>> ListTicket(ListTicketRequest request);
		Task<List<StatusSummaryResponse>> GetStatusSummary();
	}
}
