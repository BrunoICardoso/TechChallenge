using BurgerRoyale.Domain.DTO;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IProductService
	{
		Task<ProductDTO> AddAsync(RequestProductDTO addProductRequestDTO);

		Task<ProductDTO> GetByIdAsync(Guid id);

		Task<ProductDTO> UpdateAsync(Guid id, RequestProductDTO updateProductRequestDTO);

		Task RemoveAsync(Guid id);
	}
}