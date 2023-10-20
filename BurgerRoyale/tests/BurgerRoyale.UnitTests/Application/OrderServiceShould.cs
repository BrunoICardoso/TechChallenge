using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using Moq;
using Xunit;

namespace BurgerRoyale.UnitTests.Application
{
    public class OrderServiceShould
    {
        private readonly Mock<IOrderRepository> orderRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IUserRepository> userRepositoryMock;

        private readonly IOrderService orderService;
        public OrderServiceShould()
        {
            orderRepositoryMock = new Mock<IOrderRepository>();
            productRepositoryMock = new Mock<IProductRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            orderService = new OrderService(orderRepositoryMock.Object, productRepositoryMock.Object, userRepositoryMock.Object);
        }

        [Fact]
        public async Task Create_New_Order()
        {
            #region Arrange(Given)

            // Usuário
            var userId = Guid.NewGuid();
            var user = new User("52998224725", "Mike", "mike@gmail.com", BurgerRoyale.Domain.Enumerators.UserType.Customer);

            userRepositoryMock
                .Setup(x => x.GetByIdAsync(userId))
                .ReturnsAsync(user);

            //Produto
            var productId = Guid.NewGuid();
            var product = new Product("Burger", "Big burger", 20, BurgerRoyale.Domain.Enumerators.ProductCategory.Lanche);

            productRepositoryMock
                .Setup(x => x.GetByIdAsync(productId))
                .ReturnsAsync(product);

            CreateOrderProductDTO orderProduct = new CreateOrderProductDTO()
            {
                ProductId = productId,
                Quantity = 1
            };

            var orderProducts = new List<CreateOrderProductDTO>
            {
                orderProduct
            };

            //Pedido
            CreateOrderDTO orderDTO = new()
            {
                UserId = userId,
                OrderProducts = orderProducts
            };

            #endregion Arrange(Given)

            #region Act(When)

            var exception = await Record.ExceptionAsync(async () => await orderService.CreateAsync(orderDTO));

            #endregion Act(When)

            #region Assert(Then)
            Assert.Null(exception);

            orderRepositoryMock
                .Verify(
                    repository => repository.AddAsync(It.Is<Order>(order =>
                        order.OrderProducts.Count() == 1 &&
                        order.UserId == orderDTO.UserId)),
                    Times.Once());

            #endregion Assert(Then)
        }

        [Fact]
        public async Task Get_Orders()
        {
            #region Arrange(Given)

            // Produto
            var productId = Guid.NewGuid();
            var productName = "Test";
            var productDesc = "Test description";
            var orderId = Guid.NewGuid();
            decimal productPrice = 30;
            int quantity = 1;
            var productCategory = ProductCategory.Sobremesa;

            var product = new Product(productName, productDesc, productPrice, productCategory);


            // Order product
            OrderProduct orderProduct = new OrderProduct(orderId, productId, productPrice, quantity, product);

            var orderProducts = new List<OrderProduct>
            {
                orderProduct
            };

            var orderTime = DateTime.Now;
            var userId = Guid.NewGuid();
            var orderStatus = OrderStatus.EmPreparacao;

            //Pedido
            Order order = new()
            {
                UserId = userId,
                Status = orderStatus,
                OrderTime = orderTime,
                CloseTime = null,
                OrderProducts = orderProducts
            };

            var orderList = new List<Order>
            {
                order
            };

            orderRepositoryMock
                .Setup(x => x.GetOrders(null))
                .ReturnsAsync(orderList);

            #endregion Arrange(Given)

            #region Act(When)

            var orders = await orderService.GetOrdersAsync(null);

            #endregion Act(When)

            #region Assert(Then)

            orderRepositoryMock
                .Verify(
                    repository => repository.GetOrders(null),
                    Times.Once());

            Assert.NotNull(orders);
            Assert.Single(orders);
            Assert.Equal(30, orders.FirstOrDefault().TotalPrice);
            Assert.Equal(OrderStatus.EmPreparacao.GetDescription(), orders.FirstOrDefault().Status);

            #endregion Assert(Then)
        }
    }
}
