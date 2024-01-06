using BurgerRoyale.Application.ExternalServices.Payment.Interface;
using BurgerRoyale.Application.ExternalServices.Payment.Services;
using Moq;
using Xunit;

namespace BurgerRoyale.UnitTests.Application.ExternalServices;

public class PaymentServiceShould
{
    private Mock<HttpClient> httpClientMock;

    private IPaymentService paymentService;

    public PaymentServiceShould()
    {
        httpClientMock = new Mock<HttpClient>();

        paymentService = new PaymentService(httpClientMock.Object);
    }

    [Fact]
    public async Task Send_Payment()
    {
		#region Arrange(Given)

		Guid orderId = Guid.NewGuid();

		decimal price = 100;

        var x = new HttpClient();

        #endregion

        #region Act(When)

        await paymentService.Send(orderId, price);

        #endregion

        #region Assert(Then)

        var expectedURL = "/api/payments";

        httpClientMock.
            Verify(api => api.PostAsync(
                It.Is<string>(url => url.Contains(expectedURL)), It.IsAny<HttpContent>()), 
            Times.Once);

        #endregion
    }
}