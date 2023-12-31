﻿using Core.Constants;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Sockets;

namespace BusinessLogic.Services
{
    public partial class TicketService : ITicketServices
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILoggingService _log;

        public TicketService(
			IUnitOfWork unitOfWork, 
			ILoggingService log)
        {
			_unitOfWork = unitOfWork;
			_log = log;
        }

        public async Task<Ticket?> AddTicket(AddTicketRequest request)
		{
            Ticket ticket = new Ticket();

			ticket.UserId = request.UserId;
			ticket.ProductId = request.ProductId;
			ticket.CategoryId = request.CategoryId;
			ticket.PriorityId = request.PriorityId;
			ticket.Summary = request.Summary;
			ticket.Description = request.Description;
			ticket.RaisedDate = DateTime.Now;
			ticket.StatusId = DefaultStatusConstants.NEW;

			int SLA = await GetSLA(ticket.PriorityId);
			ticket.ExpectedDate = DateTime.Now.AddHours(SLA);

			_unitOfWork.Repository<Ticket>().Add(ticket);
			int result = await _unitOfWork.SaveChanges();
			if (result > 0)
				return ticket;

			return null;
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

        public async Task<List<ListTicketResponse>> ListTicketResponse(ListTicketRequest listTicketRequest)
		{
            var dbResult =  await _unitOfWork.TicketRepository.ListTicket(listTicketRequest);
			var result = dbResult.Select(x => new ListTicketResponse
			{
				TicketId = x.TicketId,
				Summary = x.Summary,
				Product = x.Product?.ProductName,
				Category = x.Category?.CategoryName,
				Priority = x.Priority?.PriorityName,
				Status = x.Status?.Name,
				RaisedBy = x.User?.Name,
				RaisedDate = x.RaisedDate,
				ExpectedDate = x.ExpectedDate
			})
			.OrderByDescending(x => x.RaisedDate)
			.ToList();

			return result;
		}

        public async Task<DefaultResponse> LockTicketRow(Ticket ticket, int UserId)
        {
			int maxTimeTickedLocked = 20;
            if (ticket.LockedUserId is not null && 
				ticket.LockedDate != null &&
                ticket.LockedUserId != UserId &&
                ticket.LockedDate.Value.AddMinutes(maxTimeTickedLocked) > DateTime.Now)
            {
				//Ticket is locked by other user
				return new DefaultResponse() {
					IsSuccess = false,
					Message = $"Ticket is locked by {ticket.LockedUser?.Name}"
				};
			}

            ticket.LockedUserId = UserId; 
			ticket.LockedDate =	DateTime.Now;

            _unitOfWork.Repository<Ticket>().Update(ticket);
            await _unitOfWork.SaveChangesReturnBool();

            return new DefaultResponse()
            {
                IsSuccess = true,
            };
        }

        public async Task<DefaultResponse> UnlockTicketByUser(int UserId)
        {
			var status = await _unitOfWork.TicketRepository.UnlockTicketByUserId(UserId);

			return new DefaultResponse
			{
				IsSuccess = status
			};
        }


        public async Task<bool> UploadFile(List<Attachment> attachments, int ticketId)
		{
			var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(ticketId);
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
