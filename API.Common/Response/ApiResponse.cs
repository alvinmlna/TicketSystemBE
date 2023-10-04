
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API.Common.Response
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }

		public object ErrorMessage { get; set; }

		public object Result { get; set; }

        public ApiResponse()
        {
        }

		public ApiResponse(int statusCode)
		{
			StatusCode = statusCode;
		}

		public ApiResponse(HttpStatusCode statusCode, object result = null, object errorMessage = null)
		{
			StatusCode = (int)statusCode;
			Result = result;
			ErrorMessage = errorMessage;
		}

		public ApiResponse ApiValidationResponse(FluentValidation.Results.ValidationResult validationResult)
		{
			var errorMessage = validationResult.Errors.Select(x => x.ErrorMessage);

			return new ApiResponse(HttpStatusCode.BadRequest, "Validation Failed", errorMessage);
		}
	}
}
