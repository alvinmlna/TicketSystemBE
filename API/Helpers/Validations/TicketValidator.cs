using API.DTO;
using FluentValidation;

namespace API.Helpers.Validations
{
	public class TicketValidator : AbstractValidator<TicketDTO>
	{
		private readonly int _maxFileSize;
		private readonly string[] _allowedExtensions;

		public TicketValidator(int maxFileSize, string[] allowedExtensions)
		{
			_maxFileSize = maxFileSize;
			_allowedExtensions = allowedExtensions;

			RuleForEach(x => x.Attachments).SetValidator(new FileValidator(_maxFileSize, _allowedExtensions));
		}



	}
}
