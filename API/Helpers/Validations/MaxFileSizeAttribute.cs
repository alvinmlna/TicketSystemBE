using Core.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace API.Helpers.Validations
{
	public class MaxFileSizeAttribute : ValidationAttribute
	{
		private readonly int _maxFileSize;
		private readonly IConfigurationService _configurationService;

		public MaxFileSizeAttribute(IConfigurationService configurationService)
		{
			_configurationService = configurationService;
			_maxFileSize = _configurationService.GetMaxFileSizeConfiguration().Result;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var file = value as List<IFormFile>;
			if (file != null)
			{
				foreach (var f in file)
				{
					if (f.Length > _maxFileSize)
					{
						return new ValidationResult(GetErrorMessage());
					}
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"Maximum allowed file size is {_maxFileSize} bytes.";
		}
	}
}
