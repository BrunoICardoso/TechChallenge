using BurgerRoyale.Application.Models;
using BurgerRoyale.Domain.DTO;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IProductService
	{
		Task<ProductResponse> AddAsync(ProductDTO addProductRequestDTO);

		Task<GetProductResponse> GetByIdAsync(Guid id);

		Task<ProductResponse> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO);

		Task<ProductResponse> RemoveAsync(Guid id);
	}
}