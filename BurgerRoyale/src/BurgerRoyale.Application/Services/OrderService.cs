using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;

namespace BurgerRoyale.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPaymentRepository _paymentRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IPaymentRepository paymentRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<int> CreateAsync(CreateOrderDTO orderDTO)
    {
        Order order = await CreateOrder(orderDTO);

        await AddOrderProductsToOrder(orderDTO, order);

        order.SetOrderNumber(await GenerateOrderNumber());

        await _orderRepository.AddAsync(order);

        await _paymentRepository.SendAsync(order.Id, order.Price);

        return order.OrderNumber;
    }

    private async Task<Order> CreateOrder(CreateOrderDTO orderDTO)
    {
        if (UserIsDefined(orderDTO))
        {
            var user = await _userRepository.GetByIdAsync(orderDTO.UserId!.Value);
            ValidateIfUserDoesNotExist(user);

            return CreateOrderWithUser(orderDTO.UserId.Value);
        }

        return CreateOrderWithoutUser();
    }

    private static bool UserIsDefined(CreateOrderDTO orderDTO)
    {
        return orderDTO.UserId.HasValue && orderDTO.UserId != Guid.Empty;
    }

    private static void ValidateIfUserDoesNotExist(User? user)
    {
        if (user is null)
            throw new NotFoundException("Usuário não encontrado.");
    }

    private static Order CreateOrderWithUser(Guid userId)
    {
        return new Order(userId);
    }
    
    private static Order CreateOrderWithoutUser()
    {
        return new Order(Guid.Empty);
    }

    private async Task AddOrderProductsToOrder(CreateOrderDTO orderDTO, Order order)
    {
        foreach (var orderProduct in orderDTO.OrderProducts)
        {
            var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);
            ValidateIfProductDoesNotExist(product);

            var newOrderProduct = new OrderProduct(order.Id, orderProduct.ProductId, product!.Price, orderProduct.Quantity);

            order.AddProduct(newOrderProduct);
        }
    }

    private static void ValidateIfProductDoesNotExist(Product? product)
    {
        if (product is null)
            throw new NotFoundException("Produto(s) inválido(s).");
    }

    public async Task<int> GenerateOrderNumber()
    {
        var anyUnclosedOrders = await _orderRepository.AnyAsync(x => x.Status == OrderStatus.Finalizado);
        if (anyUnclosedOrders)
        {
            var lastOrder = (await _orderRepository.GetAllAsync()).OrderByDescending(x => x.OrderTime).FirstOrDefault();
            return lastOrder.OrderNumber + 1;
        }
        return 1;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderStatus? orderStatus)
    {
        var orders = await _orderRepository.GetOrders(orderStatus);

        var orderDTOs = orders.Select(order => new OrderDTO(order)).ToList();

        return orderDTOs;
    }

    public async Task RemoveAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        ValidateIfOrderDoesNotExist(order);

        _orderRepository.Remove(order!);
    }

    public async Task UpdateOrderStatusAsync(Guid id, OrderStatus orderStatus)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        ValidateIfOrderDoesNotExist(order);
        
        ValidateIfStatusIsTheSame(orderStatus, order);

        order!.SetStatus(orderStatus);

        await _orderRepository.UpdateAsync(order);
    }

    private static void ValidateIfOrderDoesNotExist(Order? order)
    {
        if (order is null)
            throw new DomainException("Pedido inválido.");
    }

    private static void ValidateIfStatusIsTheSame(OrderStatus orderStatus, Order order)
    {
        if (order.Status == orderStatus)
            throw new DomainException($"Pedido já possui status {orderStatus.GetDescription()}");
    }
}