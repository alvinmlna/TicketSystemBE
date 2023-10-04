namespace Core.Interfaces.Services
{
	public interface IConfigurationService
	{
		Task<string> GetConfigurationValue(string key);
	}
}
