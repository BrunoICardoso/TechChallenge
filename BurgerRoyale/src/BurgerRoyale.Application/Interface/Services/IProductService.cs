using BurgerRoyale.Application.DTO;
using BurgerRoyale.Application.Models;

namespace BurgerRoyale.Application.Interface.Services
{
    public interface IProductService
    {
        Task<ProductResponse> AddAsync(ProductDTO addProductRequestDTO);
        
        Task<GetProductResponse> GetByIdAsync(Guid id);

        Task<ProductResponse> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO);
        
        Task<ProductResponse> RemoveAsync(Guid id);
    }
}