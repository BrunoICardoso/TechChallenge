using FakePaymentService.API.Controllers.Payments;
using FakePaymentService.API.Controllers.Payments.Requests;
using FakePaymentService.Domain.Dtos;
using FakePaymentService.Domain.Entities;
using FakePaymentService.Domain.Interface.Services;
using FakePaymentService.UnitTests.Domain.EntitiesMocks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FakePaymentService.UnitTests.API.Controllers.Payments
{
	public class PaymentsControllerTests
	{
		private readonly Mock<IPaymentService> _paymentService;

		private readonly PaymentsController _controller;

		public PaymentsControllerTests()
		{
			_paymentService = new Mock<IPaymentService>();

			_controller = new PaymentsController(_paymentService.Object);
		}

		[Fact]
		public async Task GivenPaymentRequest_WhenRequestPayment_ThenShouldReturnStatusCodeCreatedAndDTO()
		{
			#region Arrange

			Payment paymentRequest = PaymentMock.Get();
			PaymentDTO paymentRequestDTO = new PaymentDTO(paymentRequest);

			CreatePaymentRequest request = new CreatePaymentRequest(
				paymentRequest.Amount,
				paymentRequest.ClientIdentifier,
				paymentRequest.CallbackUrl
			);

			_paymentService
				.Setup(x => x.RequestPaymentAsync(
					paymentRequest.Amount,
					paymentRequest.ClientIdentifier,
					paymentRequest.CallbackUrl
				))
				.ReturnsAsync(paymentRequestDTO);

			#endregion Arrange

			#region Act

			var response = await _controller.RequestPayment(request) as ObjectResult;

			#endregion Act

			#region Assert

			response?.StatusCode.Should().Be((int)HttpStatusCode.Created);
			response?.Value.Should().Be(paymentRequestDTO);

			_paymentService
				.Verify(
					x => x.RequestPaymentAsync(
						paymentRequest.Amount,
						paymentRequest.ClientIdentifier,
						paymentRequest.CallbackUrl
					),
					Times.Once
				);

			#endregion Assert
		}

		[Fact]
		public async Task GivenPaymentRequestId_WhenGetPaymentRequest_ThenShouldReturnStatusCodeOkAndDTO()
		{
			#region Arrange

			Payment paymentRequest = PaymentMock.Get();
			PaymentDTO paymentRequestDTO = new PaymentDTO(paymentRequest);

			_paymentService
				.Setup(x => x.GetPaymentAsync(paymentRequest.Id))
				.ReturnsAsync(paymentRequestDTO);

			#endregion Arrange

			#region Act

			var response = await _controller.GetPaymentRequest(paymentRequest.Id) as ObjectResult;

			#endregion Act

			#region Assert

			response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
			response?.Value.Should().Be(paymentRequestDTO);

			_paymentService
				.Verify(
					x => x.GetPaymentAsync(paymentRequest.Id),
					Times.Once
				);

			#endregion Assert
		}

		[Fact]
		public async Task GivenPaymentRequestId_WhenMake_ThenShouldReturnStatusCodeOk()
		{
			#region Arrange

			Guid paymentRequestId = Guid.NewGuid();

			#endregion Arrange

			#region Act

			var response = await _controller.MakePayment(paymentRequestId) as ObjectResult;

			#endregion Act

			#region Assert

			response?.StatusCode.Should().Be((int)HttpStatusCode.OK);

			_paymentService
				.Verify(
					x => x.MakePaymentAsync(paymentRequestId),
					Times.Once
				);

			#endregion Assert
		}
	}
}
