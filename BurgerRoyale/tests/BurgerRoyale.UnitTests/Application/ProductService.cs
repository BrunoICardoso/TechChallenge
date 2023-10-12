using BurgerRoyale.Domain.Interface.Repositories;

namespace BurgerRoyale.UnitTests.Application
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<AddProductResponse> AddAsync(AddProductRequestDTO addProductRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}