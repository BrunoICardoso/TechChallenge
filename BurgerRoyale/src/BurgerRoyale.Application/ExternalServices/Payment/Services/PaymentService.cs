using BurgerRoyale.Application.ExternalServices.Payment.Interface;

namespace BurgerRoyale.Application.ExternalServices.Payment.Services;

public class PaymentService : IPaymentService
{
    public readonly HttpClient _apiClient;

    public PaymentService(HttpClient apiClient)
    {
        _apiClient = apiClient;
    }

    public Task Send(Guid orderId, decimal price)
    {
        throw new NotImplementedException();
    }
}