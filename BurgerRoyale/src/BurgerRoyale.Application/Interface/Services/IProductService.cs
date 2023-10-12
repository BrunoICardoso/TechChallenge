using BurgerRoyale.Application.DTO;

namespace BurgerRoyale.Application.Interface.Services
{
    public interface IProductService
    {
        Task<AddProductResponse> AddAsync(AddProductRequestDTO addProductRequestDTO);
    }
}