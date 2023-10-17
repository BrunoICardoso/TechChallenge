namespace BurgerRoyale.Domain.DTO
{
    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public IEnumerable<OrderProductDTO> OrderProducts { get; set; } = Enumerable.Empty<OrderProductDTO>();
    }
}