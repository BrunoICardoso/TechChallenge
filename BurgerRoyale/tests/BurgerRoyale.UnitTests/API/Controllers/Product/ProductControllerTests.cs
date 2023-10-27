using BurgerRoyale.API.Controllers.Product;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using BurgerRoyale.UnitTests.Domain.EntitiesMocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace BurgerRoyale.UnitTests.API.Controllers.Product
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productService;

        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productService = new Mock<IProductService>();

            _productController = new ProductController(_productService.Object);
        }

        [Fact]
        public async Task GivenProductCategory_WhenGetProducts_ThenShouldReturnListWithStatusOk()
        {
            // arrange
            _productService
                .Setup(x => x.GetListAsync(ProductCategory.Lanche))
                .ReturnsAsync(new List<ProductDTO>());

            // act
            var response = await _productController.GetList(ProductCategory.Lanche) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response?.Value.Should().BeOfType<ReturnAPI<IEnumerable<ProductDTO>>>();
        }

        [Fact]
        public async Task GivenCreateOrderDto_WhenCreateOrder_ThenShouldReturnStatusCreated()
        {
            // arrange
            var product = ProductMock.Get();

            var request = new RequestProductDTO()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category
            };

            // act
            var response = await _productController.Add(request) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task GivenProductId_WhenGetById_ThenShouldReturnProductWithStatusOk()
        {
            // arrange
            var id = Guid.NewGuid();

            // act
            var response = await _productController.GetById(id) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenProductId_WhenUpdate_ThenShouldReturnProductWithStatusOk()
        {
            // arrange
            var id = Guid.NewGuid();

            var product = ProductMock.Get(id);

            var request = new RequestProductDTO()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category
            };

            // act
            var response = await _productController.Update(id, request) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenProductId_WhenRemove_ThenShouldReturnStatusNoContent()
        {
            // arrange
            var id = Guid.NewGuid();

            // act
            var response = await _productController.Remove(id) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
    }
}
