using Core.Entities;

namespace Core.Interfaces.Repository
{
	public interface IConfigurationRepository : IGenericRepository<Configuration>
	{
		Task<Configuration?> GetByKeyAsync(string key);
	}
}
