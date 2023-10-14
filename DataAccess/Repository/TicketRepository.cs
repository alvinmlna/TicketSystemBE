using Core.DTO.Request;
using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Repository
{
	public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
	{
		public TicketRepository(TicketDBContext ticketContext) : base(ticketContext)
		{
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
				.FirstOrDefaultAsync(x => x.TicketId == id);
		}

		public Task<List<Ticket>> ListTicket(ListTicketRequest request)
		{
			return dbContext.Set<Ticket>()
				.Include(x => x.Attachments)
				.Include(x => x.Category)
				.Include(x => x.Priority)
				.Include(x => x.Product)
				.Include(x => x.Status)
				.Include(x => x.User)
				.Include(x => x.AssignedTo)
				.Where(x => (request.Summary == null || EF.Functions.Like(x.Summary, $"%{request.Summary}%")))
				.Where(x => (request.ProductId == null || request.ProductId.Contains(x.ProductId)))
				.Where(x => (request.CategoryId == null || request.CategoryId.Contains(x.CategoryId)))
				.Where(x => (request.PriorityId == null || request.PriorityId.Contains(x.PriorityId)))
				.Where(x => (request.StatusId == null || request.StatusId.Contains(x.StatusId)))
				.Where(x => (request.RaisedBy == null || request.RaisedBy.Contains(x.UserId)))
				.ToListAsync();
		}
	}
}
