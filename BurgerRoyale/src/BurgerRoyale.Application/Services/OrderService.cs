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

		public async Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderStatus? orderStatus)
		{
			var orders = await _orderRepository.GetOrders(orderStatus);

			var orderDTOs = orders.Select(order => new OrderDTO()
			{
				OrderId = order.Id,
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
			}).ToList();

			for (int i = 0; i < orderDTOs.Count(); i++)
				if (orderDTOs[i].OrderProducts != null && orderDTOs[i].OrderProducts.Count() > 0)
					foreach (var product in orderDTOs[i].OrderProducts)
						if (product != null)
							orderDTOs[i].TotalPrice += product.Price * product.Quantity;

			return orderDTOs;
		}

		public async Task RemoveAsync(Guid id)
		{
			var order = await _orderRepository.FindFirstDefaultAsync(x => x.Id == id);

			if (order is null)
				throw new DomainException("Pedido inválido.");

			_orderRepository.Remove(order);
		}

		public async Task UpdateOrderStatusAsync(Guid id, OrderStatus orderStatus)
		{
			var order = await _orderRepository.FindFirstDefaultAsync(x => x.Id == id);

			if (order is null)
				throw new DomainException("Pedido inválido.");

			if (order.Status == orderStatus)
				throw new DomainException($"Pedido já possui status {orderStatus.GetDescription()}");

			order.Status = orderStatus;
			await _orderRepository.UpdateAsync(order);
		}
	}
}
