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


		public static BadRequestObjectResult ActionFailed(object data, string message = "Action failed") 
		{
			return new BadRequestObjectResult(new ApiResponse(System.Net.HttpStatusCode.BadRequest, result: message, data: data));
		}

		public static NotFoundObjectResult NotFound(object data, string message = "Object not found!")
		{
			return new NotFoundObjectResult(new ApiResponse(System.Net.HttpStatusCode.NotFound, result: message, data: data));
		}
	}
}
