using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
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

		public async Task<IEnumerable<ProductDTO>> GetListAsync(ProductCategory? category)
		{
			var products = (category == null)
				? await _productRepository.GetAllAsync()
				: await _productRepository.FindAsync(x => x.Category == category);

			return products.Select(product => new ProductDTO(product));
		}

		public async Task<ProductDTO> AddAsync(RequestProductDTO addProductRequestDTO)
		{
			Product product = CreateProduct(addProductRequestDTO);

			await _productRepository.AddAsync(product!);

			return new ProductDTO(product!);
		}

		private static Product CreateProduct(RequestProductDTO productDTO)
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

			return new ProductDTO(product!);
		}

		private static void ThrowExceptionIfProductDoesNotExit(Product? product)
		{
			if (product is null)
			{
				throw new NotFoundException("O produto não foi encontrado");
			}
		}

		public async Task<ProductDTO> UpdateAsync(Guid id, RequestProductDTO updateProductRequestDTO)
		{
			Product? product = await _productRepository.GetByIdAsync(id);

			ThrowExceptionIfProductDoesNotExit(product);

			UpdateProduct(product!, updateProductRequestDTO);

			await _productRepository.UpdateAsync(product!);

			return new ProductDTO(product!);
		}

		private static void UpdateProduct(Product product, RequestProductDTO updateProductRequestDTO)
		{
			product!.SetDetails(
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