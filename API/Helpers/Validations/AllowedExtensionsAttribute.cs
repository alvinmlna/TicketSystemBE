using System.ComponentModel.DataAnnotations;

namespace API.Helpers.Validations
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;
		public AllowedExtensionsAttribute(string[] extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(
		object value, ValidationContext validationContext)
		{
			var file = value as List<IFormFile>;
			if (file != null)
			{
				foreach (var f in file)
				{
					var extension = Path.GetExtension(f.FileName);
					if (!_extensions.Contains(extension.ToLower()))
					{
						return new ValidationResult(GetErrorMessage());
					}
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"This file extension is not allowed!";
		}
	}
}
