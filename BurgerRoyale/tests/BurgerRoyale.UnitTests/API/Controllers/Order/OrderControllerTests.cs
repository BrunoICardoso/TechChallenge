using BurgerRoyale.API.Controllers.Order;
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

namespace BurgerRoyale.UnitTests.API.Controllers.Order
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _orderService;

        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _orderService = new Mock<IOrderService>();

            _orderController = new OrderController(_orderService.Object);
        }

        [Fact]
        public async Task GivenGetOrdersRequest_WhenGetOrders_ThenShouldReturnListWithStatusOk()
        {
            // arrange
            _orderService
                .Setup(x => x.GetOrdersAsync(OrderStatus.EmPreparacao))
                .ReturnsAsync(new List<OrderDTO>());

            // act
            var response = await _orderController.GetOrders(OrderStatus.EmPreparacao) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response?.Value.Should().BeOfType<ReturnAPI<IEnumerable<OrderDTO>>>();
        }

        [Fact]
        public async Task GivenCreateOrderDto_WhenCreateOrder_ThenShouldReturnStatusCreated()
        {
            // arrange
            var order = OrderMock.Get();

            var request = new CreateOrderDTO()
            {
                UserId = order.UserId
            };

            // act
            var response = await _orderController.CreateOrder(request) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        }

        [Fact]
        public async Task GivenUpdateOrderStatusRequest_WhenUpdateStatus_ThenShouldReturnStatusNoContent()
        {
            // arrange
            var order = OrderMock.Get();

            // act
            var response = await _orderController.UpdateOrderStatus(order.Id, OrderStatus.Pronto) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task GivenOrderId_WhenDeleteOrder_ThenShouldReturnStatusNoContent()
        {
            // arrange
            var order = OrderMock.Get();

            // act
            var response = await _orderController.DeleteOrderAsync(order.Id) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
    }
}
