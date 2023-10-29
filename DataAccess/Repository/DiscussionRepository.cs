using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class DiscussionRepository : GenericRepository<Discussion>, IDiscussionRepository
    {
        public DiscussionRepository(TicketDBContext ticketContext) : base(ticketContext)
        {
        }

        public async Task<IReadOnlyList<Discussion>> GetDiscussionByTicketId(int ticketId)
        {
            return await dbContext.Set<Discussion>().Where(x => x.TicketId == ticketId)
                .Include(x => x.User)
                .Include(x => x.Attachments)
                .OrderBy(x => x.DateSending)
                .ToListAsync();
        }
    }
}
