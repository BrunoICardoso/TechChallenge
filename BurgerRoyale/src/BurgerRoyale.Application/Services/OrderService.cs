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

		public async Task CreateAsync(CreateOrderDTO orderDTO)
		{
			if (orderDTO.UserId != Guid.Empty)
			{
				var user = await _userRepository.GetByIdAsync(orderDTO.UserId);
				if (user is null)
					throw new NotFoundException("Usuário não encontrado.");
			}

            var order = new Order(orderDTO.UserId);

			foreach (var orderProduct in orderDTO.OrderProducts)
			{
				var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);

				if (product is null)
					throw new NotFoundException("Produto(s) inválido(s).");

				var newOrderProduct = new OrderProduct(order.Id, orderProduct.ProductId, product.Price, orderProduct.Quantity);

				order.AddProduct(newOrderProduct);
			}

			await _orderRepository.AddAsync(order);
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
