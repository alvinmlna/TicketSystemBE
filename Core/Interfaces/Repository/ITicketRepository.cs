using Core.Entities;

namespace Core.Interfaces.Repository
{
	public interface ITicketRepository : IGenericRepository<Ticket>
	{
		Task<Ticket> GetTicketById(int id);
	}
}
