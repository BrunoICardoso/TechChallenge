using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
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

        public async Task CreateAsync(CreateOrderDTO orderDTO)
        {
            if (orderDTO.UserId != Guid.Empty)
            {
                var user = await _userRepository.FindFirstDefaultAsync(x => x.Id == orderDTO.UserId);
                if (user is null)
                    throw new DomainException("Usuário não encontrado.");
            }

            var orderProducts = new List<OrderProduct>();
            foreach (var orderProduct in orderDTO.OrderProducts)
            {
                var product = await _productRepository.FindFirstDefaultAsync(x => x.Id == orderProduct.ProductId);

                if (product is null)
                    throw new DomainException("Produto(s) inválido(s).");

                var newOrderProduct = new OrderProduct(new Guid(), orderProduct.ProductId, product.Price, orderProduct.Quantity);
                orderProducts.Add(newOrderProduct);
            }

            var order = new Order() { OrderTime = DateTime.Now, Status = OrderStatus.Recebido, UserId = orderDTO.UserId, OrderProducts = orderProducts };
            await _orderRepository.AddAsync(order);
        }

        public Task<OrderDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderStatus? orderStatus)
        {
            var orders = await _orderRepository.GetOrders(orderStatus);

            var orderDTOs = orders.Select(order => new OrderDTO()
            {
                Status = order.Status.GetDescription(),
                UserId = order.UserId,
                OrderTime = order.OrderTime,
                CloseTime = order.CloseTime,
                OrderProducts = order.OrderProducts.Select(x => new OrderProductDTO()
                {
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.ProductPrice

                })
            });
            return orderDTOs;
        }

        public Task<OrderDTO> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> UpdateAsync(Guid id, OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
