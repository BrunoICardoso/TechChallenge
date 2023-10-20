using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using Xunit;

namespace BurgerRoyale.UnitTests.Domain.Validation
{
	public class ProductShould
	{
		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void Validate_When_Does_Not_Have_Name(string name)
		{
			#region Act(When)

			DomainException result = Assert.Throws<DomainException>(() => new Product(name, "", 0, ProductCategory.Lanche));

			#endregion Act(When)

			#region Assert(Then)

			Assert.Equal("The name is required!", result.Message);

			#endregion Assert(Then)
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void Validate_When_Does_Not_Have_Price(decimal price)
		{
			#region Act(When)

			DomainException result = Assert.Throws<DomainException>(() => new Product("Name", "", price, ProductCategory.Lanche));

			#endregion Act(When)

			#region Assert(Then)

			Assert.Equal("The price is invalid!", result.Message);

			#endregion Assert(Then)
		}
	}
}