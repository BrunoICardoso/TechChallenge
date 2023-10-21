using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;

namespace BurgerRoyale.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<ProductDTO> AddAsync(ProductDTO addProductRequestDTO)
		{
			Product product = CreateProduct(addProductRequestDTO);

			await _productRepository.AddAsync(product!);

			addProductRequestDTO.Id = product.Id;

			return addProductRequestDTO;
		}

		private static Product CreateProduct(ProductDTO productDTO)
		{
			return new Product(
				productDTO.Name,
				productDTO.Description,
				productDTO.Price,
				productDTO.Category);
		}

		public async Task<ProductDTO> GetByIdAsync(Guid id)
		{
			Product? product = await _productRepository.GetByIdAsync(id);

			ThrowExceptionIfProductDoesNotExit(product);

			return CreateProductDTO(product!);
		}

		private static void ThrowExceptionIfProductDoesNotExit(Product? product)
		{
			if (product is null)
			{
				throw new NotFoundException("O produto não foi encontrado");
			}
		}

		private static ProductDTO CreateProductDTO(Product product)
		{
			return new ProductDTO
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Category = product.Category
			};
		}

		public async Task<ProductDTO> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO)
		{
			Product? product = await _productRepository.GetByIdAsync(id);

			ThrowExceptionIfProductDoesNotExit(product);

			Product? productUpdated = CreatedUpdatedProduct(product!.Id, updateProductRequestDTO);

			await _productRepository.UpdateAsync(productUpdated!);

			return updateProductRequestDTO;
		}

		private static Product CreatedUpdatedProduct(Guid productId, ProductDTO updateProductRequestDTO)
		{
			return new Product(
				productId,
				updateProductRequestDTO.Name,
				updateProductRequestDTO.Description,
				updateProductRequestDTO.Price,
				updateProductRequestDTO.Category);
		}

		public async Task RemoveAsync(Guid id)
		{
			Product? product = await _productRepository.GetByIdAsync(id);

			ThrowExceptionIfProductDoesNotExit(product);

			_productRepository.Remove(product!);
		}
	}
}