using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;

namespace BurgerRoyale.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateAsync(CreateOrderDTO orderDTO)
        {
            if (orderDTO.UserId.HasValue && orderDTO.UserId != Guid.Empty)
            {
                var user = await _userRepository.GetByIdAsync(orderDTO.UserId.Value);
                if (user is null)
                    throw new NotFoundException("Usuário não encontrado.");
            }

            var order = new Order(orderDTO.UserId ?? Guid.Empty);

            foreach (var orderProduct in orderDTO.OrderProducts)
            {
                var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);

                if (product is null)
                    throw new NotFoundException("Produto(s) inválido(s).");

                var newOrderProduct = new OrderProduct(order.Id, orderProduct.ProductId, product.Price, orderProduct.Quantity);

                order.AddProduct(newOrderProduct);
            }

            order.SetOrderNumber(await GenerateOrderNumber());

            await _orderRepository.AddAsync(order);
            return order.OrderNumber;
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

            if (order is null)
                throw new DomainException("Pedido inválido.");

            _orderRepository.Remove(order);
        }

        public async Task UpdateOrderStatusAsync(Guid id, OrderStatus orderStatus)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order is null)
                throw new DomainException("Pedido inválido.");

            if (order.Status == orderStatus)
                throw new DomainException($"Pedido já possui status {orderStatus.GetDescription()}");

            order.SetStatus(orderStatus);

            await _orderRepository.UpdateAsync(order);
        }
    }
}
