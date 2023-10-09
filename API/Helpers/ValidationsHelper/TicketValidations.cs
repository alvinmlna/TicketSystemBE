using API.DTO;
using API.Helpers.Validations;
using Core.Interfaces.Services;
using FluentValidation.Results;

namespace API.Helpers.ValidationsHelper
{
	public class TicketValidations
	{
		private readonly IConfigurationService _configuration;
		private readonly int _maxFileSize;
		private readonly string[] _AllowedExtensions;

		public TicketValidations(IConfigurationService configuration)
        {
			//_configuration = configuration;
			//_maxFileSize =  _configuration.GetMaxFileSizeConfiguration().Result;
			//_AllowedExtensions =  _configuration.GetAllowedExtensionsConfiguration().Result;
		}

		//public ValidationResult Validate(TicketDTO ticket)
		//{
		//	//var ticketValidation = new TicketValidator(_maxFileSize, _AllowedExtensions);
		//	//return ticketValidation.Validate(ticket);
		//}
    }
}
