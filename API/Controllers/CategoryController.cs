using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class CategoryController : BaseApiController
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService,
			IMapper mapper)
        {
			_categoryService = categoryService;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<ActionResult<CategoryDTO>> GetAll()
		{
			var u = User;
			var result = await _categoryService.GetAllAsync();
			if (result.Count == 0)
				return Ok(new List<CategoryDTO>());

			var dtoResult = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDTO>> (result);
			return Ok(dtoResult);
		}
    }
}
