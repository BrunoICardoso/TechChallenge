using BurgerRoyale.Domain.Base;
using BurgerRoyale.Domain.Entities;

namespace BurgerRoyale.UnitTests.Domain.Validation
{
    public class ProductShould
    {
        [Fact]
        public void Validate_When_Does_Not_Have_Name()
        {
            #region Arrange(Given)
            #endregion

            #region Act(When)

            DomainException result = Assert.Throws<DomainException>(() => new Product("", "", 0, Guid.NewGuid()));

            #endregion

            #region Assert(Then)

            Assert.Equal("The name is required!", result.Message);

            #endregion
        }
    }
}