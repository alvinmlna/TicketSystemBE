using API.Common.Response;
using Core.DTO.Request;
using Core.DTO.Response;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Authorization;
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


		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login(AuthRequest request)
		{
			var loginStatus = await _authService.Login(request);

			if (loginStatus.IsSuccess == false)
				return BadRequest(loginStatus.Message);

			return Ok(loginStatus);
		}

		[HttpGet, Authorize]
		public async Task<ActionResult<DefaultResponse>> AuthAccount()
		{
			return new DefaultResponse()
			{
				IsSuccess = true
			};
		}
    }
}
