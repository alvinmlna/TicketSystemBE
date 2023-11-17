using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.CahcedRepository
{
    public class CachedDiscussionRepository : GenericRepository<Discussion>, IDiscussionRepository
    {
        private readonly DiscussionRepository _discussionRepository;
        private readonly IMemoryCache _memoryCache;
        TimeSpan expiredCacheTime = TimeSpan.FromMinutes(1);

        public CachedDiscussionRepository(TicketDBContext ticketContext, 
            DiscussionRepository discussionRepository, 
            IMemoryCache memoryCache
        ) : base(ticketContext)
        {
            _discussionRepository = discussionRepository;
            _memoryCache = memoryCache;
        }

        public Task<IReadOnlyList<Discussion>> GetDiscussionByTicketId(int ticketId)
        {
            string key = $"discussion-{ticketId}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _discussionRepository.GetDiscussionByTicketId(ticketId);
                })!;
        }
    }
}
