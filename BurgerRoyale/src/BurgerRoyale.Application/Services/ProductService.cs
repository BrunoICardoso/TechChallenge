using BurgerRoyale.Application.DTO;
using BurgerRoyale.Application.Interface.Services;
using BurgerRoyale.Domain.Base;
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
            Product? product = null;

            var response = new AddProductResponse();

            try
            {
                product  = CreateProduct(addProductRequestDTO);
            } 
            catch (DomainException ex)
            {
                response.AddNotification("Validation", ex.Message);
            }

            if (response.IsValid)
            {
                await _productRepository.AddAsync(product!);
            }

            return response;
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