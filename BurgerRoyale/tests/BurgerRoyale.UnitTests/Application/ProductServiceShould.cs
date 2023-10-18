using BurgerRoyale.Application.Models;
using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using Moq;

namespace BurgerRoyale.UnitTests.Application
{
	public class ProductServiceShould
	{
		private readonly Mock<IProductRepository> productRepositoryMock;

		private readonly IProductService productService;

		public ProductServiceShould()
		{
			productRepositoryMock = new Mock<IProductRepository>();

			productService = new ProductService(productRepositoryMock.Object);
		}

		[Fact]
		public async Task Add_New_Product()
		{
			#region Arrange(Given)

			string name = "Bacon burger";
			ProductCategory category = ProductCategory.Lanche;
			string description = "Delicious bacon burger";
			decimal price = 20;

			ProductDTO addProductRequestDTO = new()
			{
				Name = name,
				Category = category,
				Description = description,
				Price = price,
			};

			#endregion Arrange(Given)

			#region Act(When)

			ProductResponse response = await productService.AddAsync(addProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);
			Assert.True(response.IsValid);

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

			ProductDTO addProductRequestDTO = new()
			{
				Name = name,
				Category = category,
				Description = description,
				Price = price,
			};

			#endregion Arrange(Given)

			#region Act(When)

			Exception? threwException = null;

			try
			{
				ProductResponse response = await productService.AddAsync(addProductRequestDTO);
			} 
			catch(Exception ex)
			{
				threwException = ex;
			}


			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(threwException);
			Assert.Equal(typeof(DomainException), threwException.GetType());

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

			GetProductResponse response = await productService.GetByIdAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);
			Assert.True(response.IsValid);
			Assert.NotNull(response.Product);

			Assert.Equal(product.Name, response.Product.Name);
			Assert.Equal(product.Price, response.Product.Price);
			Assert.Equal(product.Category, response.Product.Category);

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Return_Notification_When_Product_Does_Not_Exist()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(() => null);

			#endregion Arrange(Given)

			#region Act(When)

			GetProductResponse response = await productService.GetByIdAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			Assert.False(response.IsValid);
			Assert.Equal("The product does not exist", response.Notifications.First().Message);

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

			ProductDTO updateProductRequestDTO = new()
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

			ProductResponse response = await productService.UpdateAsync(productId, updateProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);

			Assert.True(response.IsValid);

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
		public async Task Return_Notification_When_Update_Product_That_Does_Not_Exist()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			ProductDTO updateProductRequestDTO = new();

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(() => null);

			#endregion Arrange(Given)

			#region Act(When)

			ProductResponse response = await productService.UpdateAsync(productId, updateProductRequestDTO);

			#endregion Act(When)

			#region Assert(Then)

			Assert.False(response.IsValid);
			Assert.Equal("The product does not exist", response.Notifications.First().Message);

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

			ProductResponse response = await productService.RemoveAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			Assert.NotNull(response);
			Assert.True(response.IsValid);

			productRepositoryMock
				.Verify(repository => repository.Remove(product),
				Times.Once);

			#endregion Assert(Then)
		}

		[Fact]
		public async Task Return_Notification_When_Remove_Product_That_Does_Not_Exist()
		{
			#region Arrange(Given)

			Guid productId = Guid.NewGuid();

			productRepositoryMock
				.Setup(repository => repository.GetByIdAsync(productId))
				.ReturnsAsync(() => null);

			#endregion Arrange(Given)

			#region Act(When)

			ProductResponse response = await productService.RemoveAsync(productId);

			#endregion Act(When)

			#region Assert(Then)

			Assert.False(response.IsValid);
			Assert.Equal("The product does not exist", response.Notifications.First().Message);

			productRepositoryMock
				.Verify(repository => repository.Remove(It.IsAny<Product>()),
				Times.Never);

			#endregion Assert(Then)
		}
	}
}