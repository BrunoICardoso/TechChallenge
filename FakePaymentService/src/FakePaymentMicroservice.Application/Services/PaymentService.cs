using FakePaymentService.Domain.Dtos;
using FakePaymentService.Domain.Entities;
using FakePaymentService.Domain.Enumerators;
using FakePaymentService.Domain.Exceptions;
using FakePaymentService.Domain.Interface.Repositories;
using FakePaymentService.Domain.Interface.Services;

namespace FakePaymentService.Application.Services;

public class PaymentService : IPaymentService
{
	private readonly IPaymentRepository _paymentRepository;
	private readonly INotificationService _notificationService;

	public PaymentService
	(
		IPaymentRepository paymentRepository,
		INotificationService notificationService
	)
	{
		_paymentRepository = paymentRepository;
		_notificationService = notificationService;
	}

	public async Task<PaymentDTO> RequestPaymentAsync(decimal amount, Guid? clientIdentifier, string? callbackUrl)
	{
		var paymentRequest = new Payment(amount, clientIdentifier, callbackUrl);

		await _paymentRepository.AddAsync(paymentRequest);

		return new PaymentDTO(paymentRequest);
	}

	public async Task MakePaymentAsync(Guid paymentRequestId)
	{
		var paymentRequest = await _paymentRepository.FindFirstDefaultAsync(
			x => x.Id == paymentRequestId && x.Status == PaymentStatus.Pending
		);

		if (paymentRequest is null)
			throw new NotFoundException("Pending payment request not found");

		paymentRequest.Pay();

		await _paymentRepository.UpdateAsync(paymentRequest);

		if (!string.IsNullOrWhiteSpace(paymentRequest.CallbackUrl))
		{
			await _notificationService.NotifyPaymentAsync(paymentRequest.CallbackUrl);
		}
	}

	public async Task<PaymentDTO> GetPaymentAsync(Guid paymentRequestId)
	{
		var paymentRequest = await _paymentRepository.GetByIdAsync(paymentRequestId);

		if (paymentRequest is null)
			throw new NotFoundException("Payment request not found");

		return new PaymentDTO(paymentRequest);
	}
}
