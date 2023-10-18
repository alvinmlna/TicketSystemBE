using API.DTO;
using Core.DTO.Request;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

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
		public async Task<ActionResult<string>> Login(AuthRequest request)
		{
			var loginStatus = await _authService.Login(request);

			if (loginStatus.IsSuccess == false)
				return BadRequest(loginStatus.Message);

			return Ok(loginStatus.Message);
		}
	}
}
