using Bogus;
using Bogus.Extensions;
using FakePaymentService.Domain.Entities;

namespace FakePaymentService.UnitTests.Domain.EntitiesMocks
{
	public static class PaymentMock
	{
		public static Payment Get()
		{
			return new Faker<Payment>()
				.CustomInstantiator(faker => new Payment(
					faker.Random.Decimal2(10, 100),
					faker.Random.Guid(),
					faker.Internet.UrlWithPath()
				));
		}
	}
}
