using BurgerRoyale.Domain.Exceptions;

namespace BurgerRoyale.Domain.Base
{
	public class AssertionConcern
	{
		public static void AssertArgumentLength(string stringValue, int maximum, string message)
		{
			int length = stringValue.Trim().Length;
			if (length > maximum)
			{
				throw new DomainException(message);
			}
		}

		public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
		{
			int length = stringValue.Trim().Length;
			if (length < minimum || length > maximum)
			{
				throw new DomainException(message);
			}
		}

		public static void AssertArgumentNotEmpty(string stringValue, string message)
		{
			if (stringValue == null || stringValue.Trim().Length == 0)
			{
				throw new DomainException(message);
			}
		}

		public static void AssertArgumentNotNull(object object1, string message)
		{
			if (object1 == null)
			{
				throw new DomainException(message);
			}
		}

		public static void AssertArgumentHasValidPrice(decimal price, string message)
		{
			if (price == 0 || price < 0)
			{
				throw new DomainException(message);
			}
		}

		public static void AssertArgumentHasValidGuid(Guid guidValue, string message)
		{
			if (guidValue == Guid.Empty)
			{
				throw new DomainException(message);
			}
		}
	}
}