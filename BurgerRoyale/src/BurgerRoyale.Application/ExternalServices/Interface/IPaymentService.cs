namespace BurgerRoyale.Application.ExternalServices.Interface;

public interface IPaymentService
{
    void Send(Guid orderId, decimal price);
}