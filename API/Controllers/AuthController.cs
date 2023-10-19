using API.Common.Response;
using Core.DTO.Request;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
	public class AuthController : BaseApiController
	{
		public static User user = new User();
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(RegisterUserRequest request)
		{
			var result = await _authService.Register(request);
			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<ActionResult<ApiResponse>> Login(AuthRequest request)
		{
			var loginStatus = await _authService.Login(request);

			if (loginStatus.IsSuccess == false)
				return BadRequest(loginStatus.Message);

			return new ApiResponse
			{
				StatusCode = (int)HttpStatusCode.OK,
				Data = loginStatus.Message
			};
		}
	}
}
