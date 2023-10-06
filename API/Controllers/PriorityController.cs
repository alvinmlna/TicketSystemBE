using API.DTO;
using AutoMapper;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class PriorityController : BaseApiController
	{
		private readonly IPriorityService _priorityService;
		private readonly IMapper _mapper;

		public PriorityController(IPriorityService priorityService,
			IMapper mapper)
		{
			_priorityService = priorityService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<PriorityDTO>> GetAll()
		{
			var result = await _priorityService.GetAllAsync();
			if (result.Count == 0)
				return Ok(new List<PriorityDTO>());

			var dtoResult = _mapper.Map<IReadOnlyList<Priority>, IReadOnlyList<PriorityDTO>>(result);
			return Ok(dtoResult);
		}
	}
}
