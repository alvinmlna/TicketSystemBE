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
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
			_authService = authService;
            _userService = userService;
        }

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(RegisterUserRequest request)
		{
			var result = await _authService.Register(request);
			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponse>> Login(AuthRequest request)
		{
			var loginStatus = await _authService.Login(request);

			if (loginStatus.IsSuccess == false)
				return BadRequest(loginStatus.Message);

			return Ok(loginStatus);
		}
    }
}
