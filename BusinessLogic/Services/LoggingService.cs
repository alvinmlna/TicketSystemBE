using Core.Interfaces.Services;
using Serilog;

namespace BusinessLogic.Services
{
	public class LoggingService : ILoggingService
	{
		private readonly ILogger _logger;

		public LoggingService(ILogger logger)
        {
			_logger = logger;
		}

		public void Error(string message)
		{
			_logger.Error(message);
		}

		public void Error(string message, Exception exception)
		{
			_logger.Error(message, exception.Message, exception.StackTrace);
		}

		public void Info(string message)
		{
			_logger.Information(message);
		}
	}
}
