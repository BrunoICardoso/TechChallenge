namespace BurgerRoyale.Application.ExternalServices.Payment.Models;

public class PaymentRequest
{
    public decimal Amount { get; set; }

    public Guid ClientIdentifier { get; set; }

    public string CallbackUrl { get; set; } = string.Empty;
}