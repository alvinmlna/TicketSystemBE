
using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
	{
		public ConfigurationRepository(TicketDBContext ticketContext) : base(ticketContext)
		{
		}

		public async Task<Configuration?> GetByKeyAsync(string key)
		{
			return await dbContext.Set<Configuration>().FirstOrDefaultAsync(x => x.ConfigKey == key);
		}
	}
}
