using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderProductDTO> OrderProducts { get; set; } = Enumerable.Empty<OrderProductDTO>();
    }
}
