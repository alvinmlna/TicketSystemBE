using API.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
	public class ApiResponseHelpers
	{
		public static BadRequestObjectResult ValidationError(FluentValidation.Results.ValidationResult validationResult)
		{
			return new BadRequestObjectResult(new ApiResponse().ApiValidationResponse(validationResult));
		}
	}
}
