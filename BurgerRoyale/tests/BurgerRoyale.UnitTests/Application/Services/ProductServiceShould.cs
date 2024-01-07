using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BurgerRoyale.UnitTests.Application.Services
{
	public class ProductServiceShould
	{
		private readonly Mock<IProductRepository> productRepositoryMock;
		private readonly Mock<IProductImageRepository> productImageRepositoryMock;

		private readonly IProductService productService;

		public ProductServiceShould()
		{
			productRepositoryMock = new Mock<IProductRepository>();
			productImageRepositoryMock = new Mock<IProductImageRepository>();

			productService = new ProductService(productRepositoryMock.Object, productImageRepositoryMock.Object);
		}

		[Fact]
		public async Task Add_New_Product()
		{
			#region Arrange(Given)

			string name = "Bacon burger";
			ProductCategory category = ProductCategory.Lanche;
			string description = "Delicious bacon burger";
			decimal price = 20;

			RequestProductDTO addProductRequestDTO = new()
			{
				Name = name,
				Category = category,
				Description = description,
				Price = price,
			};

			#endregion Arrange(Given)

			#region Act(When)

			ProductDTO response = await productService.AddAsync(addProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);
			Assert.NotEqual(Guid.Empty, response.Id);

			productRepositoryMock
				.Verify(
					repository => repository.AddAsync(It.Is<Product>(product =>
						product.Name == name &&
						product.Category == category &&
						product.Description == description &&
						product.Price == price)),
					Times.Once());

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Throw_Domain_Exception_When_Request_Is_Invalid()
		{
			#region Arrange(Given)

			string name = string.Empty;
			ProductCategory category = ProductCategory.Lanche;
			string description = "";
			decimal price = 0;

			RequestProductDTO addProductRequestDTO = new()
			{
				Name = name,
				Category = category,
				Description = description,
				Price = price,
			};

			#endregion Arrange(Given)

			#region Act(When)

			Func<Task> task = async () => await productService.AddAsync(addProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			await task.Should()
				.ThrowAsync<DomainException>()
				.WithMessage("The name is required!");

			productRepositoryMock
				.Verify(repository => repository.AddAsync(It.IsAny<Product>()),
				Times.Never());

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Get_By_Id()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			var product = new Product("Bacon burger", "", 100, ProductCategory.Lanche);

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(product);

			#endregion Arrange(Given)

			#region Act(When)

			ProductDTO response = await productService.GetByIdAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);

			Assert.Equal(product.Name, response.Name);
			Assert.Equal(product.Price, response.Price);
			Assert.Equal(product.Category, response.Category);

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Throw_Not_Found_Exception_When_Product_Does_Not_Exist()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(() => null);

			#endregion Arrange(Given)

			#region Act(When)

			Func<Task> task = async () => await productService.GetByIdAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			await task.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("O produto não foi encontrado");

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Update_Product()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			string newName = "New Bacon burger 2.0";
			ProductCategory newCategory = ProductCategory.Lanche;
			string newDescription = "new and still delicious bacon burger";
			decimal newPrice = 40;

			RequestProductDTO updateProductRequestDTO = new()
			{
				Name = newName,
				Category = newCategory,
				Description = newDescription,
				Price = newPrice,
			};

			var product = new Product(productId, "Bacon burger", "", 100, newCategory);

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(product);

			#endregion Arrange(Given)

			#region Act(When)

			ProductDTO response = await productService.UpdateAsync(productId, updateProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);

			productRepositoryMock
				.Verify(repository => repository.UpdateAsync(It.Is<Product>(product =>
					product.Id == productId &&
					product.Name == newName &&
					product.Category == newCategory &&
					product.Description == newDescription &&
					product.Price == newPrice)),
				Times.Once);

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Return_Exception_When_Update_Product_That_Does_Not_Exist()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			RequestProductDTO updateProductRequestDTO = new();

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(() => null);

			#endregion Arrange(Given)

			#region Act(When)

			Func<Task> task = async () => await productService.UpdateAsync(productId, updateProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			await task.Should()
				.ThrowAsync<NotFoundException>();

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Remove_Product()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			var product = new Product(productId, "Bacon burger", "", 100, ProductCategory.Lanche);

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(product);

			#endregion Arrange(Given)

			#region Act(When)

			await productService.RemoveAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			productRepositoryMock
				.Verify(repository => repository.Remove(product),
				Times.Once);

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Return_Exception_When_Remove_Product_That_Does_Not_Exist()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(() => null);

			#endregion Arrange(Given)

			#region Act(When)

			Func<Task> task = async () => await productService.RemoveAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			await task.Should()
				.ThrowAsync<NotFoundException>();

			productRepositoryMock
				.Verify(repository => repository.Remove(It.IsAny<Product>()),
				Times.Never);

			#endregion Assert(Then)
		}

		[Fact]
		public async Task GivenNoCategory_WhenGetListOfProducts_ThenShouldGetAllProducts()
		{
			// arrange
			var products = new List<Product> {
				new Product("Product 1", "", 100, ProductCategory.Lanche),
				new Product("Product 2", "", 100, ProductCategory.Acompanhamento)
			};

			productRepositoryMock
				.Setup(repository => repository.GetAll())
				.ReturnsAsync(products);

			// act
			var response = await productService.GetListAsync(null);

			// assert
			response.Should().BeAssignableTo<IEnumerable<ProductDTO>>();
			response.Should().HaveCount(2);

			productRepositoryMock.Verify(
				x => x.GetAll(),
				Times.Once()
			);

			productRepositoryMock.Verify(
				x => x.FindAsync(It.IsAny<Expression<Func<Product, bool>>>()),
				Times.Never()
			);
		}

		[Fact]
		public async Task GivenCategory_WhenGetListOfProducts_ThenShouldFindProductsOfCategory()
		{
			// arrange
			var products = new List<Product> {
				new Product("Product 1", "", 100, ProductCategory.Lanche),
				new Product("Product 2", "", 100, ProductCategory.Lanche)
			};

			productRepositoryMock
				.Setup(repository => repository.GetAll())
				.ReturnsAsync(products);

            productRepositoryMock
                .Setup(repository => repository.GetAllByCategory(ProductCategory.Lanche))
                .ReturnsAsync(products);

            // act
            var response = await productService.GetListAsync(ProductCategory.Lanche);

			// assert
			response.Should().BeAssignableTo<IEnumerable<ProductDTO>>();
			response.Should().HaveCount(2);

			productRepositoryMock.Verify(
				x => x.GetAllByCategory(ProductCategory.Lanche),
				Times.Once()
			);

			productRepositoryMock.Verify(
				x => x.GetAllAsync(),
				Times.Never()
			);
		}
	}
}