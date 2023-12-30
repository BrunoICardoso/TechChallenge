using FakePaymentService.Domain.Dtos;

namespace FakePaymentService.Domain.Interface.Services
{
	public interface IPaymentService
	{
		Task<PaymentDTO> RequestPaymentAsync(decimal amount, Guid? clientIdentifier, string? callbackUrl);

		Task MakePaymentAsync(Guid paymentRequestId);

		Task<PaymentDTO> GetPaymentAsync(Guid paymentRequestId);
	}
}
