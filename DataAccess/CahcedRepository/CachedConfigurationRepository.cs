using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.CahcedRepository
{
    public class CachedConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConfigurationRepository _configurationRepository;
        TimeSpan expiredCacheTime = TimeSpan.FromMinutes(2);

        public CachedConfigurationRepository(
            TicketDBContext ticketContext, 
            ConfigurationRepository configurationRepository, 
            IMemoryCache memoryCache) 
        : base(ticketContext)
        {
            _configurationRepository = configurationRepository;
            _memoryCache = memoryCache;
        }

        public Task<Configuration?> GetByKeyAsync(string key)
        {
            string cahceKey = $"configuration-{key}";

            return _memoryCache.GetOrCreateAsync(
                cahceKey,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _configurationRepository.GetByKeyAsync(key);
                })!;
        }
    }
}
