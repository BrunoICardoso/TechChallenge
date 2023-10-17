namespace BurgerRoyale.Domain.DTO
{
    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public IEnumerable<CreateOrderProductDTO> OrderProducts { get; set; } = Enumerable.Empty<CreateOrderProductDTO>();
    }
}