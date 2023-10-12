using BurgerRoyale.Application.DTO;
using BurgerRoyale.Application.Models;

namespace BurgerRoyale.Application.Interface.Services
{
    public interface IProductService
    {
        Task<AddProductResponse> AddAsync(ProductDTO addProductRequestDTO);
        
        Task<GetProductResponse> GetByIdAsync(Guid id);

        Task<UpdateProductResponse> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO);
    }
}