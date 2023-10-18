using BurgerRoyale.Domain.DTO;

namespace BurgerRoyale.Domain.Interface.Services
{
    public interface IProductService
	{
		Task<ProductDTO> AddAsync(ProductDTO addProductRequestDTO);

		Task<ProductDTO> GetByIdAsync(Guid id);

		Task<ProductDTO> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO);

		Task RemoveAsync(Guid id);
	}
}