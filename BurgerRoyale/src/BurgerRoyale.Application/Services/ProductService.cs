using BurgerRoyale.Application.DTO;
using BurgerRoyale.Application.Interface.Services;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;

namespace BurgerRoyale.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddProductResponse> AddAsync(AddProductRequestDTO addProductRequestDTO)
        {
            Product product = CreateProduct(addProductRequestDTO);

            await _productRepository.AddAsync(product);

            return new AddProductResponse();
        }

        private static Product CreateProduct(AddProductRequestDTO addProductRequestDTO)
        {
            return new Product(
                addProductRequestDTO.Name, 
                addProductRequestDTO.Description, 
                addProductRequestDTO.Price, 
                addProductRequestDTO.CategoryId);
        }
    }
}