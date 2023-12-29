using FakePaymentService.Domain.Dtos;

namespace FakePaymentService.Domain.Interface.Services
{
	public interface IPaymentService
	{
		Task<PaymentDTO> RequestPayment(decimal amount, Guid? clientIdentifier, string? callbackUrl);

		Task MakePayment(Guid paymentRequestId);

		Task<PaymentDTO> GetPayment(Guid paymentRequestId);
	}
}
