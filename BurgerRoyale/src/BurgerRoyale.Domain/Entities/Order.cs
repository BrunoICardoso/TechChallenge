using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Entities;

public class Order : Entity
{
    public Guid UserId { get; private set; }
    public DateTime OrderTime { get; private set; }
    public DateTime? CloseTime { get; private set; }
    public OrderStatus Status { get; private set; }
    public int OrderNumber { get; private set; }

    public decimal Price
    {
        get
        {
            return OrderProducts.Sum(x => x.ProductPrice * x.Quantity);
        }
    }

    public virtual List<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();

    public Order(Guid userId)
    {
        UserId = userId;
        OrderTime = DateTime.Now;
        Status = OrderStatus.Recebido;
    }

    public void AddProduct(OrderProduct orderProduct)
    {
        OrderProducts.Add(orderProduct);
    }

    public void SetOrderNumber(int orderNumber)
    {
        OrderNumber = orderNumber;
    }
    public void SetStatus(OrderStatus status)
    {
        Status = status;
        if (status == OrderStatus.Finalizado)
            CloseOrder();
        else
            OpenOrder();
    }

    public void CloseOrder()
    {
        CloseTime = DateTime.Now;
    }

    public void OpenOrder()
    {
        CloseTime = null;
    }
}