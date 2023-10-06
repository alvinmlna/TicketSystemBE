using API.DTO;
using AutoMapper;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class UserController : BaseApiController
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UserController(IUserService userService,
			IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<UserDTO>> GetAll()
		{
			var result = await _userService.GetAllAsync();
			if (result.Count == 0)
				return Ok(new List<UserDTO>());

			var dtoResult = _mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDTO>>(result);
			return Ok(dtoResult);
		}

		[HttpGet("admin")]
		public async Task<ActionResult<UserDTO>> GetAllAdmin()
		{
			var result = await _userService.GetAllAdminAsync();
			if (result.Count == 0)
				return Ok(new List<UserDTO>());

			var dtoResult = _mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDTO>>(result);
			return Ok(dtoResult);
		}
	}
}
