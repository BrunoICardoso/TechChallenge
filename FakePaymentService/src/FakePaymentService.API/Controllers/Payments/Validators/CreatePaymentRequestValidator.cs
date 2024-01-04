using FakePaymentService.API.Controllers.Payments.Requests;
using FluentValidation;

public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
	public CreatePaymentRequestValidator()
	{
		When(w => w is not null, () =>
		{
			RuleFor(r => r.Amount)
				.NotEmpty()
				.GreaterThan(0)
				.WithMessage("Valid amount must be informed");

			RuleFor(r => r.ClientIdentifier)
				.NotEmpty()
				.When(r => r.ClientIdentifier != null);

			RuleFor(r => r.CallbackUrl)
				.Must(r => Uri.TryCreate(r, UriKind.Absolute, out _))
				.When(r => r.CallbackUrl != null)
				.WithMessage("Valid callback url must be informed");
		});
	}
}
