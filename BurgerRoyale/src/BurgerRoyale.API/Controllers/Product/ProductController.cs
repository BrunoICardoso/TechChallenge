using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetList([FromQuery] ProductCategory? productCategory)
		{
			IEnumerable<ProductDTO> response = await _productService.GetListAsync(productCategory);

			return IStatusCode(new ReturnAPI<IEnumerable<ProductDTO>>(response));
		}

		[HttpPost]
		[ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Add([FromBody] RequestProductDTO productDTO)
		{
			ProductDTO response = await _productService.AddAsync(productDTO);

			return IStatusCode(new ReturnAPI<ProductDTO>(HttpStatusCode.Created, response));
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			ProductDTO response = await _productService.GetByIdAsync(id);

			return IStatusCode(new ReturnAPI<ProductDTO>(response));
		}

		[HttpPut("{id:Guid}")]
		[ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RequestProductDTO productDTO)
		{
			ProductDTO response = await _productService.UpdateAsync(id, productDTO);

			return IStatusCode(new ReturnAPI<ProductDTO>(response));
		}

		[HttpDelete("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> Remove([FromRoute] Guid id)
		{
			await _productService.RemoveAsync(id);

			return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
		}
	}
}