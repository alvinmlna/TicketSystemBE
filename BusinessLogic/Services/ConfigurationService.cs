using BusinessLogic.Helpers;
using Core.Constants;
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

		public async Task<string[]> GetAllowedExtensionsConfiguration()
		{
			var result = await GetConfigurationValue(ConfigContants.FileUpload_AllowedExtensions);
			if (result == null) return new string[] { };

			return result.ConfigValue.Trim().Split(',');
		}

		//Default will be 0 if no result
		public async Task<int> GetMaxFileSizeConfiguration()
		{
			var result = await GetConfigurationValue(ConfigContants.FileUpload_MaxFileSize);
			if (result == null) return 0;

			if (int.TryParse(result?.ConfigValue, out var integerValue))
			{
				return integerValue;
			}
			else
			{
				return 0;
			}
		}

		private async Task<Configuration?> GetConfigurationValue(string key, bool searchCache = true)
		{
			if (searchCache)
			{
				Configuration fromCache = (Configuration)MemoryCacheHelpers.GetFromMemoryCache(key);
				if (fromCache != null) return fromCache;
			}

			var fromDb = await _unitOfWork.ConfigurationRepository.GetByKeyAsync(key);
            if (fromDb != null)
			{
				MemoryCacheHelpers.AddToMemoryCache(key, fromDb, DateTimeOffset.Now.AddHours(1));
			}

            return fromDb;
		}
	}
}
