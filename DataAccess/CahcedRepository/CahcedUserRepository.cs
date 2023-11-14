using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.CahcedRepository
{
    public class CahcedUserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        TimeSpan expiredCacheTime = TimeSpan.FromMinutes(2);

        public CahcedUserRepository(TicketDBContext ticketContext, UserRepository userRepository, IMemoryCache memoryCache) : base(ticketContext)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public Task<User> GetUserById(int id)
        {
            string key = $"user-{id}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _userRepository.GetUserById(id);
                })!;
        }

        public Task<IReadOnlyList<User>> ListAllUsers(string search = "")
        {
            string key = $"user-list-{search}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _userRepository.ListAllUsers(search);
                })!;
        }
    }
}
