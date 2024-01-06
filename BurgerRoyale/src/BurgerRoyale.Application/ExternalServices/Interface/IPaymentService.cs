namespace BurgerRoyale.Application.ExternalServices.Interface;

public interface IPaymentService
{
    Task Send(Guid orderId, decimal price);
}