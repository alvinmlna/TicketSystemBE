using FluentValidation;

namespace API.Helpers.Validations
{
	public class FileValidator : AbstractValidator<IFormFile>
	{
		private readonly string[] _acceptedExtensions;

		public FileValidator(int maxFileSize, string[] acceptedExtensions )
		{
			_acceptedExtensions = acceptedExtensions;

			RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(maxFileSize)
				.WithMessage("File size is larger than allowed");

			RuleFor(x => x.FileName).NotNull().Must(x => ValidateExtensions(x))
				.WithMessage("This file extension is not allowed!");
		}

		public bool ValidateExtensions(string fileName)
		{
			string extension = Path.GetExtension(fileName);

			if (!_acceptedExtensions.Contains(extension.ToLower()))
			{
				return false;
			}
			return true;
		}
	}
}
