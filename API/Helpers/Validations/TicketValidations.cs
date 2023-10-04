using API.DTO;
using Core.Interfaces.Services;
using FluentValidation;

namespace API.Helpers.Validations
{
	public class TicketValidations : AbstractValidator<TicketDTO>
	{
		private readonly int _maxFileSize;
		private readonly IConfigurationService _configurationService;

		public TicketValidations(IConfigurationService configurationService)
		{
			_configurationService = configurationService;
			_maxFileSize = _configurationService.GetMaxFileSizeConfiguration().Result;

			RuleForEach(x => x.Attachments).SetValidator(new FileValidator(_maxFileSize, "maxFileSize"));
		}



	}
}
