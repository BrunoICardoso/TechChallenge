using FakePaymentService.Domain.Entities;
using FakePaymentService.Domain.Enumerators;
using FakePaymentService.Domain.Helpers;

namespace FakePaymentService.Domain.Dtos;

public class PaymentDTO
{
	public Guid PaymentId { get; private set; }
	public decimal Amount { get; private set; }
	public string Status { get; private set; }
	public bool Paid { get; private set; }
	public DateTime RequestedAt { get; private set; }
	public DateTime? UpdatedAt { get; private set; }

	public PaymentDTO(Payment payment)
	{
		PaymentId = payment.Id;
		Amount = payment.Amount;
		Paid = payment.Status == PaymentStatus.Paid;
		Status = payment.Status.GetDescription();
		RequestedAt = payment.CreatedAt;
		UpdatedAt = payment.UpdatedAt;
	}
}
