namespace FakePaymentService.API.Controllers.Payments;

public record CreatePaymentRequestViewModel
(
	decimal Amount,
	Guid? ClientIdentifier,
	string? CallbackUrl
);
