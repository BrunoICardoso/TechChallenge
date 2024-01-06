namespace BurgerRoyale.Application.ExternalServices.Payment.Interface;

public interface IPaymentService
{
    Task SendAsync(Guid orderId, decimal price);
}