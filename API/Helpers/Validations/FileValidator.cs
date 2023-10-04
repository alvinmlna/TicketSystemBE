using FluentValidation;

namespace API.Helpers.Validations
{
	public class FileValidator : AbstractValidator<IFormFile>
	{
		public FileValidator(int maxFileSize, string fileType)
		{
			RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(maxFileSize)
				.WithMessage("File size is larger than allowed");

			RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
				.WithMessage("File type is larger than allowed");
        }
	}
}
