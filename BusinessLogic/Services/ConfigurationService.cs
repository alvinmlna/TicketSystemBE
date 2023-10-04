using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
	public class ConfigurationService : IConfigurationService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ConfigurationService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

        public async Task<string> GetConfigurationValue(string key)
		{
			var config = await _unitOfWork.Repository<Configuration>().ListAllAsync();
			if (config != null)
			{
				return config.FirstOrDefault(x => x.ConfigKey == key)?.ConfigValue;
			}
			return "ada";
		}
	}
}
