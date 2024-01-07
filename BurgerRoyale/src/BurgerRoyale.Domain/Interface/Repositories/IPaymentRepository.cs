namespace BurgerRoyale.Domain.Interface.Repositories;

public interface IPaymentRepository
{
    Task SendAsync(Guid orderId, decimal price);
}