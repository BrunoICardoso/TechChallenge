using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;

namespace BurgerRoyale.Application.Services;

public class ProductService : IProductService
{
	private readonly IProductRepository _productRepository;

	private readonly IProductImageRepository _productImageRepository;

	public ProductService
	(
		IProductRepository productRepository,
		IProductImageRepository productImageRepository)
	{
		_productRepository = productRepository;
		_productImageRepository = productImageRepository;
	}

	public async Task<IEnumerable<ProductDTO>> GetListAsync(ProductCategory? category)
	{
		var products = (category is null)
			? await _productRepository.GetAll()
			: await _productRepository.GetAllByCategory(category.Value);

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
		List<ProductImage> productImagesList = new List<ProductImage>();
		if (productDTO.Images != null)
		{
			foreach (var image in productDTO.Images)
			{
				var productImage = new ProductImage(image.Title, image.Url, Guid.NewGuid());
				productImagesList.Add(productImage);
			}
		}

		Product product = new(
			productDTO.Name,
			productDTO.Description,
			productDTO.Price,
			productDTO.Category)
		{
			Images = productImagesList
		};

		return product;
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
		var currentImages = await _productImageRepository.FindAsync(x => x.ProductId == id);
		_productImageRepository.RemoveRange(currentImages);

		Product? product = await _productRepository.GetByIdAsync(id);

		ThrowExceptionIfProductDoesNotExit(product);

		UpdateProduct(product!, updateProductRequestDTO);

		await AddImagesToProductIfExist(product!);

		await _productRepository.UpdateAsync(product!);

		return new ProductDTO(product!);
	}

	private async Task AddImagesToProductIfExist(Product product)
	{
		IEnumerable<ProductImage> images = product.Images ?? Enumerable.Empty<ProductImage>();
		await _productImageRepository.AddRangeAsync(images);
	}

	private static void UpdateProduct(Product product, RequestProductDTO updateProductRequestDTO)
	{
		if (updateProductRequestDTO.Images != null)
		{
			var images = updateProductRequestDTO.Images.Select(x => new ProductImage(x.Title, x.Url, Guid.NewGuid())).ToList();
			product.Images = images;
		}
		product!.SetDetails(
			updateProductRequestDTO.Name,
			updateProductRequestDTO.Description,
			updateProductRequestDTO.Price,
			updateProductRequestDTO.Category
			);
	}

	public async Task RemoveAsync(Guid id)
	{
		Product? product = await _productRepository.GetByIdAsync(id);

		ThrowExceptionIfProductDoesNotExit(product);

		_productRepository.Remove(product!);
	}
}