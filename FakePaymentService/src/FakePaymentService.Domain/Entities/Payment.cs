using FakePaymentService.Domain.Enumerators;

namespace FakePaymentService.Domain.Entities
{
	public class Payment : Entity
	{
		public decimal Amount { get; private set; }

		public Guid? ClientIdentifier { get; private set; }

		public string? CallbackUrl { get; private set; }

		public PaymentStatus Status { get; private set; }

		public Payment(decimal amount, Guid? clientIdentifier, string? callbackUrl)
		{
			Amount = amount;
			ClientIdentifier = clientIdentifier;
			CallbackUrl = callbackUrl;
			Status = PaymentStatus.Pending;
		}

		public void Pay()
		{
			Status = PaymentStatus.Paid;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}