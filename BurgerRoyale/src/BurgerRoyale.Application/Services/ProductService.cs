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

        private static Product CreateProduct(ProductDTO productDTO)
        {
            return new Product(
                productDTO.Name, 
                productDTO.Description, 
                productDTO.Price, 
                productDTO.CategoryId);
        }

        public async Task<GetProductResponse> GetByIdAsync(Guid id)
        {
            var response = new GetProductResponse();

            Product? product = await _productRepository.GetByIdAsync(id);

            AddNotificationIfProductDoesNotExist(response, product);

            if (ResponseIsNotValid(response))
            {
                return response;
            }

            ProductDTO productDTO = CreateProductDTO(product!);

            return new GetProductResponse
            {
                Product = productDTO
            };
        }

        private static void AddNotificationIfProductDoesNotExist(GetProductResponse response, Product? product)
        {
            if (product is null)
            {
                response.AddNotification("productId", "The product does not exist");
            }
        }

        private static bool ResponseIsNotValid(GetProductResponse response)
        {
            return !response.IsValid;
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

        public async Task<UpdateProductResponse> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO)
        {
            Product? product = await _productRepository.GetByIdAsync(id);

            var newProduct = new Product(
                product.Id,
                updateProductRequestDTO.Name,
                updateProductRequestDTO.Description,
                updateProductRequestDTO.Price,
                updateProductRequestDTO.CategoryId);

            await _productRepository.UpdateAsync(newProduct);

            return new UpdateProductResponse();
        }
    }
}