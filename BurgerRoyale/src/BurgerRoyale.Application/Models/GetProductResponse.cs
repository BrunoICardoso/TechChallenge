using BurgerRoyale.Application.DTO;
using Flunt.Notifications;

namespace BurgerRoyale.Application.Models
{
    public class GetProductResponse : Notifiable<Notification>
    {
        public ProductDTO? Product { get; set; }
    }
}