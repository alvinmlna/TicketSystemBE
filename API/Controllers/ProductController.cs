using API.DTO;
using AutoMapper;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Services;
using eCommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService,
			IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<Product>> GetAll()
		{
			var result = await _productService.GetAllAsync();
			if (result.Count == 0)
				return Ok(new List<ProductDTO>());

			var dtoResult = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(result);
			return Ok(dtoResult);
		}
	}
}
