using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
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

        Assert.Equal(order.TotalPrice, expectedPrince);

        #endregion
    }
    
    [Fact]
    public void Not_Update_Status_To_Received_When_Payment_Is_Already_Aproved()
    {
		#region Arrange(Given)

		Order order = new(Guid.Empty);

        order.SetStatus(OrderStatus.PagamentoAprovado);

        #endregion

        #region Act(When)

        Exception? threwException = null;

        try
        {
            order.SetStatus(OrderStatus.Recebido);
        } catch (Exception ex)
        {
            threwException = ex;
        }

        #endregion

        #region Assert(Then)

        Assert.NotNull(threwException);
        Assert.Equal(typeof(DomainException), threwException.GetType());
        Assert.Equal("O pagamento já foi aprovado.", threwException.Message);

        #endregion
    }
}