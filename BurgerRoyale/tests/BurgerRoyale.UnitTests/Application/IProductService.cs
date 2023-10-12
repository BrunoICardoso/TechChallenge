namespace BurgerRoyale.UnitTests.Application
{
    public interface IProductService
    {
        Task<AddProductResponse> AddAsync(AddProductRequestDTO addProductRequestDTO);
    }
}