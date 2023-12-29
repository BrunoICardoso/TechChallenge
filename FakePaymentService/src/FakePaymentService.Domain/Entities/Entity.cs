namespace FakePaymentService.Domain.Entities
{
	public class Entity
	{
		public Guid Id { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; protected set; }

		public Entity()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
		}
	}
}
