using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BurgerRoyale.API.Extensions
{
    public static class AddErrosFromNotifications
    {
        public static ModelStateDictionary AddErrosFromNofifications(this ModelStateDictionary modelState, IEnumerable<Notification> notifications)
        {
            foreach (var item in notifications)
            {
                modelState.AddModelError(item.Key, item.Message);
            }

            return modelState;
        }
    }
}