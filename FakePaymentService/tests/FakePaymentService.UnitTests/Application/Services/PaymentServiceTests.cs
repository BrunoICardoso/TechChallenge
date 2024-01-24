using FakePaymentService.Application.Services;
using FakePaymentService.Domain.Entities;
using FakePaymentService.Domain.Enumerators;
using FakePaymentService.Domain.Exceptions;
using FakePaymentService.Domain.Interface.Repositories;
using FakePaymentService.Domain.Interface.Services;
using FakePaymentService.UnitTests.Domain.EntitiesMocks;
using System.Linq.Expressions;

namespace FakePaymentService.UnitTests.Application.Services
{
	public class PaymentServiceTests
	{
		private readonly Mock<IPaymentRepository> _paymentRepository;
		private readonly Mock<INotificationService> _notificationService;

		private readonly PaymentService _paymentService;

		public PaymentServiceTests()
		{
			_paymentRepository = new Mock<IPaymentRepository>();
			_notificationService = new Mock<INotificationService>();

			_paymentService = new PaymentService(
				_paymentRepository.Object,
				_notificationService.Object
			);
		}

		[Fact]
		public async Task GivenRequestPaymentInfo_WhenCreateEntity_ThenShouldReturnDTO()
		{
			#region Arrange

			decimal amount = (decimal)10.0;
			string callbackUrl = "http://localhost:8000";

			#endregion Arrange

			#region Act

			var response = await _paymentService.RequestPaymentAsync(amount, null, callbackUrl);

			#endregion Act

			#region Assert

			response.Amount.Should().Be(amount);
			response.Paid.Should().BeFalse();
			response.Status.Should().Be("Pending");

			_paymentRepository.Verify(
				x => x.AddAsync(It.Is<Payment>(p => p.Amount == amount)),
				Times.Once
			);

			#endregion Assert
		}

		[Fact]
		public async Task GivenIdOfNonExistentPaymentRequest_WhenMakePayment_ThenShouldThrowNotFoundException()
		{
			#region Arrange

			Guid paymentRequestId = Guid.NewGuid();

			#endregion Arrange

			#region Act

			Func<Task> func = async () => await _paymentService.MakePaymentAsync(paymentRequestId);

			#endregion Act

			#region Assert

			await func.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("Pending payment request not found");

			#endregion Assert
		}

		[Fact]
		public async Task GivenPaymentRequestId_WhenMakePayment_ShouldUpdatePaymentRequestStatusAndNotify()
		{
			#region Arrange

			Payment paymentRequest = PaymentMock.Get();

			_paymentRepository
				.Setup(x => x.FindFirstDefaultAsync(It.IsAny<Expression<Func<Payment, bool>>>()))
				.ReturnsAsync(paymentRequest);

			#endregion Arrange

			#region Act

			await _paymentService.MakePaymentAsync(paymentRequest.Id);

			#endregion Act

			#region Assert

			_paymentRepository
				.Verify(
					x => x.UpdateAsync(It.Is<Payment>(p => p.Status == PaymentStatus.Paid)),
					Times.Once
				);

			_notificationService
				.Verify(
					x => x.NotifyPaymentAsync(It.Is<string>(u => u == paymentRequest.CallbackUrl)),
					Times.Once
				);

			#endregion Assert
		}

		[Fact]
		public async Task GivenIdOfNonExistentPaymentRequest_WhenGetPaymentAsync_ThenShouldThrowNotFoundException()
		{
			#region Arrange

			Guid paymentRequestId = Guid.NewGuid();

			#endregion Arrange

			#region Act

			Func<Task> func = async () => await _paymentService.GetPaymentAsync(paymentRequestId);

			#endregion Act

			#region Assert

			await func.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("Payment request not found");

			#endregion Assert
		}

		[Fact]
		public async Task GivenPaymentRequestId_WhenGetPaymentAsync_ThenShouldReturnDTO()
		{
			#region Arrange

			Payment paymentRequest = PaymentMock.Get();

			_paymentRepository
				.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(paymentRequest);

			#endregion Arrange

			#region Act

			var response = await _paymentService.GetPaymentAsync(paymentRequest.Id);

			#endregion Act

			#region Assert

			response.PaymentId.Should().Be(paymentRequest.Id);
			response.Amount.Should().Be(paymentRequest.Amount);

			#endregion Assert
		}
	}
}
