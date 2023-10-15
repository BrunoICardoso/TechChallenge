using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;

namespace BurgerRoyale.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(
            IOrderRepository orderRepository,
            IOrderStatusRepository orderStatusRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderStatusRepository = orderStatusRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task CreateAsync(OrderDTO orderDTO)
        {
            if (orderDTO.UserId != Guid.Empty)
            {
                var user = await _userRepository.FindFirstDefaultAsync(x => x.Id == orderDTO.UserId);
                if (user is null)
                    throw new DomainException("Usuário não encontrado.");
            }
            var orderStatus = await _orderStatusRepository.FindFirstDefaultAsync(x => x.Description == "Recebido");
            if (orderStatus is null)
                throw new DomainException("Erro ao cadastrar status do pedido.");

            var orderProducts = new List<OrderProduct>();
            foreach (var orderProduct in orderDTO.OrderProducts)
            {
                var product = await _productRepository.FindFirstDefaultAsync(x => x.Id == orderProduct.ProductId);

                if (product is null)
                    throw new DomainException("Produto(s) inválido(s).");

                var newOrderProduct = new OrderProduct(new Guid(), orderProduct.ProductId, product.Price, orderProduct.Quantity);
                orderProducts.Add(newOrderProduct);
            }

            var order = new Order() { OrderTime = DateTime.Now, StatusId = orderStatus.Id, UserId = orderDTO.UserId, OrderProducts = orderProducts };
            await _orderRepository.AddAsync(order);
        }

        public Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> UpdateAsync(Guid id, OrderDTO updateProductRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
