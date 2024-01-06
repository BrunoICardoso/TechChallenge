namespace BurgerRoyale.Application.ExternalServices.Payment.Interface;

public interface IPaymentService
{
    Task Send(Guid orderId, decimal price);
}