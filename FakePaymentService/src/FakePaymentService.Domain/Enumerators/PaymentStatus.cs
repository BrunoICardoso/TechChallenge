using System.ComponentModel;

namespace FakePaymentService.Domain.Enumerators
{
	public enum PaymentStatus
	{
		[Description("Pending")]
		Pending,

		[Description("Paid")]
		Paid
	}
}
