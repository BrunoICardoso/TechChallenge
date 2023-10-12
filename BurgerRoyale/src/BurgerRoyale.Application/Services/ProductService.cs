using BurgerRoyale.Application.DTO;
using BurgerRoyale.Application.Interface.Services;
using BurgerRoyale.Application.Models;
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

        public async Task<AddProductResponse> AddAsync(ProductDTO addProductRequestDTO)
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

        private static Product CreateProduct(ProductDTO addProductRequestDTO)
        {
            return new Product(
                addProductRequestDTO.Name, 
                addProductRequestDTO.Description, 
                addProductRequestDTO.Price, 
                addProductRequestDTO.CategoryId);
        }

        public async Task<GetProductResponse> GetById(Guid id)
        {
            Product product = await _productRepository.GetByIdAsync(id);

            ProductDTO productDTO = CreateProductDTO(product);

            return new GetProductResponse
            {
                Product = productDTO
            };
        }

        private static ProductDTO CreateProductDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = product.Category,
                Images = product.Images,
                OrderProducts = product.OrderProducts
            };
        }
    }
}