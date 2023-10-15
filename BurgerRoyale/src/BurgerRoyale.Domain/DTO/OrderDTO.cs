namespace BurgerRoyale.Domain.DTO
{
    public class OrderDTO
    {
        public Guid UserId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? CloseTime { get; set; }
        //public decimal OrderPrice { get; private set; }
        public IEnumerable<OrderProductDTO> OrderProducts { get; set; } = Enumerable.Empty<OrderProductDTO>();
    }
}
