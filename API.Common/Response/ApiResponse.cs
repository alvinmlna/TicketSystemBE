
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API.Common.Response
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }

		public object Error { get; set; }

		public object Result { get; set; }
		public object Data { get; set; }

		public ApiResponse()
        {
        }

		public ApiResponse(int statusCode)
		{
			StatusCode = statusCode;
		}

		public ApiResponse(HttpStatusCode statusCode, object result = null, object errorMessage = null, object data = null)
		{
			StatusCode = (int)statusCode;
			Result = result;
			Error = errorMessage;
			Data = data;
		}

		public ApiResponse ApiValidationResponse(FluentValidation.Results.ValidationResult validationResult)
		{
			var errorMessage = validationResult.Errors.Select(x => x.ErrorMessage);

			return new ApiResponse(HttpStatusCode.BadRequest, "Validation Failed", errorMessage);
		}
	}
}
