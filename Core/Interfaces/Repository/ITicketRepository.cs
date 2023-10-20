﻿using Core.DTO.InternalDTO;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;

namespace Core.Interfaces.Repository
{
	public interface ITicketRepository : IGenericRepository<Discussion>
	{
		Task<Discussion> GetTicketById(int id);
		Task<List<Discussion>> ListTicket(ListTicketRequest request);
		Task<List<StatusSummaryResponse>> GetStatusSummary();


		Task<List<Last12MonthTicketFromDB>> GetLast12MonthTickets();
		Task<List<CategoryChartFromDB>> GetCategoryChart(string type);
	}
}
