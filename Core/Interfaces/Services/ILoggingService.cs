namespace Core.Interfaces.Services
{
	public interface ILoggingService
	{
		void Info(string message);
		void Error(string message);
	}
}
