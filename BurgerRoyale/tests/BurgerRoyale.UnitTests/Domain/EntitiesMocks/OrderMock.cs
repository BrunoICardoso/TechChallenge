using BurgerRoyale.Domain.Entities;

namespace BurgerRoyale.UnitTests.Domain.EntitiesMocks
{
    public static class OrderMock
    {
		public static Order Get
		(
			Guid? userId = null
		)
		{
			return new Order(userId ?? Guid.NewGuid());
		}
	}
}
