﻿using Core.Constants;
using Core.DTO.InternalDTO;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Repository
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
	{
		public TicketRepository(TicketDBContext ticketContext) : base(ticketContext)
		{
		}

		public async Task<List<CategoryChartFromDB>> GetCategoryChart(string type)
		{
			IQueryable<IGrouping<string, Ticket>> data;

			switch (type)
			{
				case "category":
					data = dbContext.Tickets.Include(x => x.Category).GroupBy(x => x.Category.CategoryName);
					break;
				case "product":
					data = dbContext.Tickets.Include(x => x.Product).GroupBy(x => x.Product.ProductName);
					break;
				case "priority":
					data = dbContext.Tickets.Include(x => x.Priority).GroupBy(x => x.Priority.PriorityName);
					break;
				default:
					data = dbContext.Tickets.Include(x => x.Category).GroupBy(x => x.Category.CategoryName);
					break;
			}

			return await data.Select(x => new CategoryChartFromDB
			{
				Key = x.Key,
				Count = x.Count().ToString()
			}).ToListAsync();
		}

		public async Task<List<Last12MonthTicketFromDB>> GetLast12MonthTickets()
		{
			var startDate = DateTime.Now.AddDays(-1 * 365);
			var allStats = dbContext
				.Tickets
				.Where(x => x.RaisedDate > startDate)
				.GroupBy(x => EF.Functions.DateDiffMonth(startDate, x.RaisedDate));

			var groupped = await allStats.Select(x => new
			{
				x.Key,
				Count = x.Count()
			})
			.ToListAsync();

			var result = groupped
			.Select(x => new Last12MonthTicketFromDB
			{
				CreatedDate = startDate.AddMonths(x.Key),
				Count = x.Count
			})
			.OrderBy(x => x.CreatedDate)
			.ToList();

			if(result.Count > 12)
			{
				result.RemoveAt(0);
			}

			return result;
		}

		public async Task<List<StatusSummaryResponse>> GetStatusSummary()
		{
			List<StatusSummaryResponse> response = new List<StatusSummaryResponse>();


			//NEW
			response.Add(new StatusSummaryResponse
			{
				Status = "New",
				Count = await dbContext.Tickets.AsNoTracking().Where(x => x.Status.StatusGroupId == StatusGroupContants.NEW).CountAsync()
			});

			//OPEN
			response.Add(new StatusSummaryResponse
			{
				Status = "Open",
				Count = await dbContext.Tickets.AsNoTracking().Where(x => x.Status.StatusGroupId == StatusGroupContants.OPEN).CountAsync()
			});

			//EXPIRED
			response.Add(new StatusSummaryResponse
			{
				Status = "Expired",
				Count = await dbContext.Tickets.AsNoTracking()
					.Where(x => x.Status.StatusGroupId == StatusGroupContants.OPEN && x.ExpectedDate <= DateTime.Now).CountAsync()
			});

			//Closed
			response.Add(new StatusSummaryResponse
			{
				Status = "Closed",
				Count = await dbContext.Tickets.AsNoTracking().Where(x => x.Status.StatusGroupId == StatusGroupContants.CLOSED).CountAsync()
			});

			//Total
			response.Add(new StatusSummaryResponse
			{
				Status = "Total",
				Count = await dbContext.Tickets.AsNoTracking().CountAsync()
			});

			return response;
		}

		public Task<Ticket> GetTicketById(int id)
		{
			return dbContext.Set<Ticket>()
				.Include(x => x.Attachments)
				.Include(x => x.Category)
				.Include(x => x.Priority)
				.Include(x => x.Product)
				.Include(x => x.Status)
				.Include(x => x.User)
				.Include(x => x.AssignedTo)
				.Include(x => x.LockedUser)
				.FirstOrDefaultAsync(x => x.TicketId == id);
		}

		public Task<List<Ticket>> ListTicket(ListTicketRequest? request)
		{
			IQueryable<Ticket> mainQuery = dbContext.Set<Ticket>()
				.Include(x => x.Attachments)
				.Include(x => x.Category)
				.Include(x => x.Priority)
				.Include(x => x.Product)
				.Include(x => x.Status)
				.Include(x => x.User)
				.Include(x => x.AssignedTo);

			if(request == null)
				return mainQuery.ToListAsync();
			
			if(!string.IsNullOrEmpty(request.Summary))
			{
				mainQuery = mainQuery
				.Where(x => (EF.Functions.Like(x.Summary, $"%{request.Summary}%")));
            }

			if (request.ProductId != null)
			{
				mainQuery = mainQuery.Where(x => request.ProductId.Contains(x.ProductId));

            }

            if (request.CategoryId != null)
            {
				mainQuery = mainQuery.Where(x => request.CategoryId.Contains(x.CategoryId));
            }

            if (request.PriorityId != null)
            {
                mainQuery = mainQuery.Where(x => request.PriorityId.Contains(x.PriorityId));
            }

            if (request.StatusId != null)
            {
                mainQuery = mainQuery.Where(x => request.StatusId.Contains(x.StatusId));
            }

            if (request.RaisedBy != null)
            {
                mainQuery = mainQuery.Where(x => request.RaisedBy.Contains(x.UserId));
            }

			return mainQuery.ToListAsync();
		}

        public async Task<bool> UnlockTicketByUserId(int userId)
        {
			var lockedByUser = await dbContext.Set<Ticket>().FirstOrDefaultAsync(x => x.LockedUserId == userId);
			if (lockedByUser != null)
			{
				lockedByUser.LockedUser = null;
				lockedByUser.LockedDate = null;
				lockedByUser.LockedUserId = null;

				dbContext.Set<Ticket>().Update(lockedByUser);
				return await dbContext.SaveChangesAsync() > 0;
			}

			return false;
        }
    }
}
