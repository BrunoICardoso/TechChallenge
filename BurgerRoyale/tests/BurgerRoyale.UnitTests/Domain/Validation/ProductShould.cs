using BurgerRoyale.Domain.Base;
using BurgerRoyale.Domain.Entities;

namespace BurgerRoyale.UnitTests.Domain.Validation
{
    public class ProductShould
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_When_Does_Not_Have_Name(string name)
        {
            #region Arrange(Given)
            #endregion

            #region Act(When)

            DomainException result = Assert.Throws<DomainException>(() => new Product(name, "", 0, Guid.NewGuid()));

            #endregion

            #region Assert(Then)

            Assert.Equal("The name is required!", result.Message);

            #endregion
        }
    }
}