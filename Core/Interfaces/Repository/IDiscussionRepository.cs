using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IDiscussionRepository : IGenericRepository<Discussion>
    {
        Task<IReadOnlyList<Discussion>> GetDiscussionByTicketId(int ticketId);
    }
}
