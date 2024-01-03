using FakePaymentService.API.Controllers.Payments.Requests;
using FakePaymentService.Domain.Dtos;
using FakePaymentService.Domain.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FakePaymentService.API.Controllers.Payments;

[Route("api/payments")]
[ApiController]
public class PaymentsController : ControllerBase
{
	private readonly IPaymentService _paymentService;

	public PaymentsController(IPaymentService paymentService)
	{
		_paymentService = paymentService;
	}

	[HttpPost]
	[SwaggerOperation(Summary = "Create new payment request", Description = "Create new payment request")]
	[ProducesResponseType(typeof(PaymentDTO), StatusCodes.Status201Created)]
	[ProducesDefaultResponseType]
	public async Task<IActionResult> RequestPayment
	(
		[FromBody] CreatePaymentRequest request
	)
	{
		var paymentRequest = await _paymentService.RequestPaymentAsync(
			request.Amount,
			request.ClientIdentifier,
			request.CallbackUrl
		);

		return Created($"/api/payments/{paymentRequest.PaymentId}", paymentRequest);
	}

	[HttpGet("{paymentRequestId:Guid}")]
	[SwaggerOperation(Summary = "Get a payment request", Description = "Get a payment request")]
	[ProducesResponseType(typeof(PaymentDTO), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType]
	public async Task<IActionResult> GetPaymentRequest([FromRoute] Guid paymentRequestId)
	{
		var paymentRequest = await _paymentService.GetPaymentAsync(paymentRequestId);

		return Ok(paymentRequest);
	}

	[HttpPost("{paymentRequestId:Guid}:pay")]
	[SwaggerOperation(Summary = "Make payment", Description = "Make payment")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesDefaultResponseType]
	public async Task<IActionResult> MakePayment([FromRoute] Guid paymentRequestId)
	{
		await _paymentService.MakePaymentAsync(paymentRequestId);

		return Ok();
	}
}
