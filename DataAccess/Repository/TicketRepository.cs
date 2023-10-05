using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

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
				.FirstOrDefaultAsync(x => x.TicketId == id);
		}
	}
}
