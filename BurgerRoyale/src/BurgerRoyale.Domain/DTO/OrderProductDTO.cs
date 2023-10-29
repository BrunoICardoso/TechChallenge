using BurgerRoyale.Domain.Entities;

namespace BurgerRoyale.Domain.DTO
{
	public class OrderProductDTO
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

        public OrderProductDTO(OrderProduct orderProduct)
        {
			ProductId = orderProduct.ProductId;
			ProductName = orderProduct.Product.Name;
			Quantity = orderProduct.Quantity;
			Price = orderProduct.ProductPrice;
        }
    }
}