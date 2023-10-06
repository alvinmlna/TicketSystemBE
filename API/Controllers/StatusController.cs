using API.DTO;
using AutoMapper;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class StatusController : BaseApiController
	{
		private readonly IStatusService _statusService;
		private readonly IMapper _mapper;

		public StatusController(IStatusService statusService,
			IMapper mapper)
		{
			_statusService = statusService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<StatusDTO>> GetAll()
		{
			var result = await _statusService.GetAllAsync();
			if (result.Count == 0)
				return Ok(new List<UserDTO>());

			var dtoResult = _mapper.Map<IReadOnlyList<Status>, IReadOnlyList<StatusDTO>>(result);
			return Ok(dtoResult);
		}
	}
}
