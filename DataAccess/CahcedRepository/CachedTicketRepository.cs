using Core.DTO.InternalDTO;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace DataAccess.CahcedRepository
{
    public class CachedTicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        TimeSpan expiredCacheTime = TimeSpan.FromMinutes(1);

        public CachedTicketRepository(TicketDBContext ticketContext,
            ITicketRepository ticketRepository,
            IMemoryCache memoryCache,
            IDistributedCache distributedCache
        ) : base(ticketContext)
        {
            _ticketRepository = ticketRepository;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        public Task<List<CategoryChartFromDB>> GetCategoryChart(string type)
        {
            string key = $"category-chart-{type}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _ticketRepository.GetCategoryChart(type);
                })!;
        }

        public Task<List<Last12MonthTicketFromDB>> GetLast12MonthTickets()
        {
            string key = $"last-12month";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _ticketRepository.GetLast12MonthTickets();
                })!;
        }

        public async Task<List<StatusSummaryResponse>> GetStatusSummary()
        {
            string key = "status-summary";

            string? cahcedData = await _distributedCache.GetStringAsync(
                key);


            List<StatusSummaryResponse> result;
            if (string.IsNullOrEmpty(cahcedData))
            {
                result = await _ticketRepository.GetStatusSummary();
                if(result is null)
                    return result;

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(result),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = expiredCacheTime
                    });

                return result;
            }

            result = JsonConvert.DeserializeObject<List<StatusSummaryResponse>>(cahcedData);

            return result;
        }

        public Task<Ticket> GetTicketById(int id)
        {
            return _ticketRepository.GetTicketById(id);
        }

        public Task<List<Ticket>> ListTicket(ListTicketRequest request)
        {
            return _ticketRepository.ListTicket(request);
        }
    }
}
