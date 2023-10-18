using BurgerRoyale.Application.Models;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using Flunt.Notifications;

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

			response.Product = productDTO;

			return response;
		}

		private static void AddNotificationIfProductDoesNotExist(Notifiable<Notification> response, Product? product)
		{
			if (product is null)
			{
				response.AddNotification("productId", "The product does not exist");
			}
		}

		private static bool ResponseIsNotValid(Notifiable<Notification> response)
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
				Category = product.Category
			};
		}

		public async Task<ProductResponse> UpdateAsync(Guid id, ProductDTO updateProductRequestDTO)
		{
			var response = new ProductResponse();

			Product? product = await _productRepository.GetByIdAsync(id);

			AddNotificationIfProductDoesNotExist(response, product);

			if (ResponseIsNotValid(response))
			{
				return response;
			}

			Product? productUpdated = null;

			try
			{
				productUpdated = CreatedUpdatedProduct(product!.Id, updateProductRequestDTO);
			}
			catch (DomainException ex)
			{
				response.AddNotification("Validation", ex.Message);
			}

			if (response.IsValid)
			{
				await _productRepository.UpdateAsync(productUpdated!);
			}

			return response;
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

		public async Task<ProductResponse> RemoveAsync(Guid id)
		{
			var response = new ProductResponse();

			Product? product = await _productRepository.GetByIdAsync(id);

			AddNotificationIfProductDoesNotExist(response, product);

			if (ResponseIsNotValid(response))
			{
				return response;
			}

			_productRepository.Remove(product!);

			return response;
		}
	}
}