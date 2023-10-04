using Core.Entities;

namespace Core.Interfaces.Services
{
	public interface IConfigurationService
	{
		Task<int> GetMaxFileSizeConfiguration();
	}
}
