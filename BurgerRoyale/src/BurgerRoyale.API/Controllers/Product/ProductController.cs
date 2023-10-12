using BurgerRoyale.API.ConfigController;
using BurgerRoyale.API.Extensions;
using BurgerRoyale.Application.Models;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BurgerRoyale.API.Controllers.Product
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : BaseController
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost]
		[ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Add([FromBody] ProductDTO productDTO)
		{
			ProductResponse response = await _productService.AddAsync(productDTO);

			if (response.IsValid)
			{
				return StatusCode(StatusCodes.Status201Created, response);
			}

			return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetById(Guid id)
		{
			GetProductResponse response = await _productService.GetByIdAsync(id);

			if (response.IsValid)
			{
				return Ok(response);
			}

			return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
		}

		[HttpPut("{id:Guid}")]
		[ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Update(Guid id, [FromBody] ProductDTO productDTO)
		{
			ProductResponse response = await _productService.UpdateAsync(id, productDTO);

			if (response.IsValid)
			{
				return Ok(response);
			}

			return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
		}

		[HttpDelete("{id:Guid}")]
		[ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Remove(Guid id)
		{
			ProductResponse response = await _productService.RemoveAsync(id);

			if (response.IsValid)
			{
				return Ok(response);
			}

			return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
		}
	}
}