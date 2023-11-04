using API.DTO;
using API.Helpers;
using AutoMapper;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("image/{fileName}")]
        public async Task<IActionResult> GetUserImage(string fileName)
        {
            fileName = Path.Combine(AppContext.BaseDirectory + "profile", fileName);
            if (!System.IO.File.Exists(fileName)) return ApiResponseHelpers.NotFound(fileName);

            Byte[] b = System.IO.File.ReadAllBytes(fileName);   // You can use your own method over here.         
            return File(b, "image/jpeg");
        }


        [HttpGet("imagebyid/{id}")]
        public async Task<IActionResult> GetUserImageById(int id)
        {
			var user = await _userService.GetUserById(id);
			if (user == null) return BadRequest(id);

            var fileName = Path.Combine(AppContext.BaseDirectory + "profile", user.ImagePath);
            if (!System.IO.File.Exists(fileName)) return ApiResponseHelpers.NotFound(fileName);

            Byte[] b = System.IO.File.ReadAllBytes(fileName);   // You can use your own method over here.         
            return File(b, "image/jpeg");
        }
    }
}
