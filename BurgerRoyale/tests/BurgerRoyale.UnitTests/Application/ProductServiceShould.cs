using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;
using Moq;

namespace BurgerRoyale.UnitTests.Application
{
    public class ProductServiceShould
    {
        [Fact]
        public async Task Add_New_Product()
        {
            #region Arrange(Given)

            string name = "Bacon burger";
            Guid categoryId = Guid.NewGuid();
            string description = "Delicious bacon burger";
            decimal price = 20;

            AddProductRequestDTO addProductRequestDTO = new()
            {
                Name = name,
                CategoryId = categoryId,
                Description = description,
                Price = price,
            };

            var productRepositoryMock = new Mock<IProductRepository>();

            IProductService productService = new ProductService(productRepositoryMock.Object);

            #endregion

            #region Act(When)

            AddProductResponse response = await productService.AddAsync(addProductRequestDTO);

            #endregion

            #region Assert(Then)

            Assert.NotNull(response);
            Assert.True(response.IsValid);

            productRepositoryMock
                .Verify(
                    repository => repository.AddAsync(It.Is<Product>(product => 
                        product.Name == name &&
                        product.CategoryId == categoryId &&
                        product.Description == description &&
                        product.Price == price)),
                    Times.Once());

            #endregion
        }
    }
}