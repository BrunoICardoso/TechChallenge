using BurgerRoyale.Domain.Entities;
using Xunit;

namespace BurgerRoyale.UnitTests.Domain.Validation;

public class OrderShould
{
    [Fact]
    public void Have_Price()
    {
		#region Arrange(Given)

		Order order = new(Guid.Empty);

        decimal product1Price = 100;
        int product1Quantity = 3;

        var product1 = new OrderProduct(order.Id, Guid.Empty, product1Price, product1Quantity);
        
        decimal product2Price = 36.5M;
        int product2Quantity = 4;

        var product2 = new OrderProduct(order.Id, Guid.Empty, product2Price, product2Quantity);

        #endregion

        #region Act(When)

        order.AddProduct(product1);
        order.AddProduct(product2);

        #endregion

        #region Assert(Then)

        decimal expectedPrince = (product1Price * product1Quantity) + (product2Price * product2Quantity);

        Assert.Equal(order.Price, expectedPrince);

        #endregion
    }
}