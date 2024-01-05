namespace BurgerRoyale.Domain.DTO;

public record CreatePaymentDto
(
	decimal Amount,
	Guid? ClientIdentifier,
	string? CallbackUrl
);
