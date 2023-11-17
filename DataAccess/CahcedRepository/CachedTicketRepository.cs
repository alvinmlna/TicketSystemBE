using Core.DTO.InternalDTO;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.CahcedRepository
{
    public class CachedTicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMemoryCache _memoryCache;
        TimeSpan expiredCacheTime = TimeSpan.FromMinutes(1);

        public CachedTicketRepository(TicketDBContext ticketContext,
            ITicketRepository ticketRepository,
            IMemoryCache memoryCache
            
            ) : base(ticketContext)
        {
            _ticketRepository = ticketRepository;
            _memoryCache = memoryCache;
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

        public Task<List<StatusSummaryResponse>> GetStatusSummary()
        {
            return _ticketRepository.GetStatusSummary();
        }

        public Task<Ticket> GetTicketById(int id)
        {
            string key = $"ticket-{id}";

            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiredCacheTime);
                    return _ticketRepository.GetTicketById(id);
                })!;
        }

        public Task<List<Ticket>> ListTicket(ListTicketRequest request)
        {
            return _ticketRepository.ListTicket(request);
        }
    }
}
