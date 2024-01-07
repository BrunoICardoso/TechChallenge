using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using Moq;
using Xunit;

namespace BurgerRoyale.UnitTests.Application.Services;

public class OrderServiceShould
{
    private readonly Mock<IOrderRepository> orderRepositoryMock;
    private readonly Mock<IProductRepository> productRepositoryMock;
    private readonly Mock<IUserRepository> userRepositoryMock;
    private readonly Mock<IPaymentRepository> paymentRepositoryMock;
    private readonly IOrderService orderService;

    public OrderServiceShould()
    {
        orderRepositoryMock = new Mock<IOrderRepository>();
        productRepositoryMock = new Mock<IProductRepository>();
        userRepositoryMock = new Mock<IUserRepository>();
        paymentRepositoryMock = new Mock<IPaymentRepository>();

        orderService = new OrderService(
            orderRepositoryMock.Object, 
            productRepositoryMock.Object, 
            userRepositoryMock.Object,
            paymentRepositoryMock.Object);
    }

    [Fact]
    public async Task Create_New_Order()
    {
        #region Arrange(Given)

        // Usuário
        var userId = Guid.NewGuid();
        var user = new User("52998224725", "Mike", "mike@gmail.com", UserType.Customer);

        userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        //Produto
        var productId = Guid.NewGuid();
        var product = new Product("Burger", "Big burger", 20, ProductCategory.Lanche);

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
    public async Task Send_Payment_Request_When_Create_Order()
    {
        #region Arrange(Given)

        var user = new User("52998224725", "Mike", "mike@gmail.com", UserType.Customer);

        userRepositoryMock
            .Setup(x => x.GetByIdAsync(user.Id))
            .ReturnsAsync(user);

        var product = new Product("Burger", "Big burger", 20, ProductCategory.Lanche);

        productRepositoryMock
            .Setup(x => x.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        CreateOrderDTO orderDTO = new()
        {
            UserId = user.Id,
            OrderProducts = new CreateOrderProductDTO[]
            {
                new()
                {
                    ProductId = product.Id,
                    Quantity = 1
                }
            }
        };

        #endregion

        #region Act(When)

        await orderService.CreateAsync(orderDTO);

        #endregion

        #region Assert(Then)

        paymentRepositoryMock
            .Verify(repository => repository.SendAsync(
                It.Is<Guid>(orderId => orderId != Guid.Empty),
                It.Is<decimal>(price => price != 0)), 
            Times.Once);

        #endregion
    }

    [Fact]
    public async Task CreateOrder_WithInvalidUser_ThenShouldGiveAnException()
    {
        #region Arrange(Given)

        // Usuário
        var userId = Guid.NewGuid();
        var user = new User("52998224725", "Mike", "mike@gmail.com", UserType.Customer);

        userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        //Produto
        var productId = Guid.NewGuid();
        var product = new Product("Burger", "Big burger", 20, ProductCategory.Lanche);

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
            UserId = Guid.NewGuid(),
            OrderProducts = orderProducts
        };

        #endregion Arrange(Given)

        #region Act(When)

        var exception = await Record.ExceptionAsync(async () => await orderService.CreateAsync(orderDTO));

        #endregion Act(When)

        #region Assert(Then)

        Assert.NotNull(exception);
        Assert.Equal("Usuário não encontrado.", exception.Message);

        #endregion Assert(Then)
    }

    [Fact]
    public async Task CreateOrder_WithInvalidProduct_ThenShouldGiveAnException()
    {
        #region Arrange(Given)

        // Usuário
        var userId = Guid.NewGuid();
        var user = new User("52998224725", "Mike", "mike@gmail.com", UserType.Customer);

        userRepositoryMock
            .Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        //Produto
        var productId = Guid.NewGuid();
        var product = new Product("Burger", "Big burger", 20, ProductCategory.Lanche);

        productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId))
            .ReturnsAsync(product);

        CreateOrderProductDTO orderProduct = new CreateOrderProductDTO()
        {
            ProductId = Guid.NewGuid(),
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

        Assert.NotNull(exception);
        Assert.Equal("Produto(s) inválido(s).", exception.Message);

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

        var userId = Guid.NewGuid();
        var orderStatus = OrderStatus.EmPreparacao;

        //Pedido
        Order order = new(userId);
        order.AddProduct(orderProduct);
        order.SetStatus(orderStatus);

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

    [Fact]
    public async Task Remove_Order()
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

        var userId = Guid.NewGuid();
        var orderStatus = OrderStatus.EmPreparacao;

        //Pedido
        Order order = new(userId);
        order.AddProduct(orderProduct);
        order.SetStatus(orderStatus);

        var orderList = new List<Order>
        {
            order
        };

        orderRepositoryMock
            .Setup(x => x.GetOrders(null))
            .ReturnsAsync(orderList);

        orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        #endregion Arrange(Given)

        #region Act(When)

        var exception = await Record.ExceptionAsync(async () => await orderService.RemoveAsync(orderId));

        #endregion Act(When)

        #region Assert(Then)

        Assert.Null(exception);

        orderRepositoryMock
            .Verify(
                repository => repository.Remove(It.Is<Order>(order =>
                    order.OrderProducts.Count() == 1 &&
                    order.UserId == order.UserId)),
                Times.Once());

        #endregion Assert(Then)
    }

    [Fact]
    public async Task RemoveOrder_UsingInvalidOrderId_ThenShouldGiveAnException()
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

        var userId = Guid.NewGuid();
        var orderStatus = OrderStatus.EmPreparacao;

        //Pedido
        Order order = new(userId);
        order.AddProduct(orderProduct);
        order.SetStatus(orderStatus);

        var orderList = new List<Order>
        {
            order
        };

        orderRepositoryMock
            .Setup(x => x.GetOrders(null))
            .ReturnsAsync(orderList);

        orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        #endregion Arrange(Given)

        #region Act(When)

        var exception = await Record.ExceptionAsync(async () => await orderService.RemoveAsync(Guid.NewGuid()));

        #endregion Act(When)

        #region Assert(Then)

        Assert.NotNull(exception);
        Assert.Equal("Pedido inválido.", exception.Message);

        orderRepositoryMock
            .Verify(
                repository => repository.Remove(It.Is<Order>(order =>
                    order.OrderProducts.Count() == 1 &&
                    order.UserId == order.UserId)),
                Times.Never());

        #endregion Assert(Then)
    }

    [Fact]
    public async Task Update_Order_Status()
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

        var userId = Guid.NewGuid();
        var orderStatus = OrderStatus.EmPreparacao;

        //Pedido
        Order order = new(userId);
        order.AddProduct(orderProduct);
        order.SetStatus(orderStatus);

        var orderList = new List<Order>
        {
            order
        };

        orderRepositoryMock
            .Setup(x => x.GetOrders(null))
            .ReturnsAsync(orderList);

        orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        #endregion Arrange(Given)

        #region Act(When)

        var exception = await Record.ExceptionAsync(async () => await orderService.UpdateOrderStatusAsync(orderId, OrderStatus.Pronto));

        #endregion Act(When)

        #region Assert(Then)

        Assert.Null(exception);

        orderRepositoryMock
            .Verify(
                repository => repository.UpdateAsync(It.Is<Order>(order =>
                    order.OrderProducts.Count() == 1 &&
                    order.UserId == order.UserId)),
                Times.Once());

        #endregion Assert(Then)
    }

    [Fact]
    public async Task UpdateStatus_WithInvalidOrderId_ThenShouldGiveAnException()
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

        var userId = Guid.NewGuid();
        var orderStatus = OrderStatus.EmPreparacao;

        //Pedido
        Order order = new(userId);
        order.AddProduct(orderProduct);
        order.SetStatus(orderStatus);

        var orderList = new List<Order>
        {
            order
        };

        orderRepositoryMock
            .Setup(x => x.GetOrders(null))
            .ReturnsAsync(orderList);

        orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        #endregion Arrange(Given)

        #region Act(When)

        var exception = await Record.ExceptionAsync(async () => await orderService.UpdateOrderStatusAsync(Guid.NewGuid(), OrderStatus.Pronto));

        #endregion Act(When)

        #region Assert(Then)

        Assert.NotNull(exception);
        Assert.Equal("Pedido inválido.", exception.Message);

        orderRepositoryMock
            .Verify(
                repository => repository.UpdateAsync(It.Is<Order>(order =>
                    order.OrderProducts.Count() == 1 &&
                    order.UserId == order.UserId)),
                Times.Never());

        #endregion Assert(Then)
    }

    [Fact]
    public async Task UpdateStatus_WithSameStatus_ThenShouldGiveAnException()
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

        var userId = Guid.NewGuid();
        var orderStatus = OrderStatus.EmPreparacao;

        //Pedido
        Order order = new(userId);
        order.AddProduct(orderProduct);
        order.SetStatus(orderStatus);

        var orderList = new List<Order>
        {
            order
        };

        orderRepositoryMock
            .Setup(x => x.GetOrders(null))
            .ReturnsAsync(orderList);

        orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        #endregion Arrange(Given)

        #region Act(When)

        var exception = await Record.ExceptionAsync(async () => await orderService.UpdateOrderStatusAsync(orderId, OrderStatus.EmPreparacao));

        #endregion Act(When)

        #region Assert(Then)

        Assert.NotNull(exception);
        Assert.Equal($"Pedido já possui status {orderStatus.GetDescription()}", exception.Message);

        orderRepositoryMock
            .Verify(
                repository => repository.UpdateAsync(It.Is<Order>(order =>
                    order.OrderProducts.Count() == 1 &&
                    order.UserId == order.UserId)),
                Times.Never());

        #endregion Assert(Then)
    }
}
