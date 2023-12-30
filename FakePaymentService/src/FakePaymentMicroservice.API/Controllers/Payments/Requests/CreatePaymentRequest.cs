namespace FakePaymentService.API.Controllers.Payments.Requests;

public record CreatePaymentRequest
(
	decimal Amount,
	Guid? ClientIdentifier,
	string? CallbackUrl
);
