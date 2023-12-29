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

	public PaymentService(IPaymentRepository paymentRepository)
	{
		_paymentRepository = paymentRepository;
	}

	public async Task<PaymentDTO> RequestPayment(decimal amount, Guid? clientIdentifier, string? callbackUrl)
	{
		var paymentRequest = new Payment(amount, clientIdentifier, callbackUrl);

		await _paymentRepository.AddAsync(paymentRequest);

		return new PaymentDTO(paymentRequest);
	}

	public async Task MakePayment(Guid paymentRequestId)
	{
		var paymentRequest = await _paymentRepository.FindFirstDefaultAsync(
			x => x.Id == paymentRequestId && x.Status == PaymentStatus.Pending
		);

		if (paymentRequest is null)
			throw new NotFoundException("Pending payment request not found");

		paymentRequest.Pay();

		await _paymentRepository.UpdateAsync(paymentRequest);
	}

	public async Task<PaymentDTO> GetPayment(Guid paymentRequestId)
	{
		var paymentRequest = await _paymentRepository.GetByIdAsync(paymentRequestId);

		if (paymentRequest is null)
			throw new NotFoundException("Payment request not found");

		return new PaymentDTO(paymentRequest);
	}
}
