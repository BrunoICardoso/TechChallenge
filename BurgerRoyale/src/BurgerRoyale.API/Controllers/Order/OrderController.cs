using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BurgerRoyale.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderStatus? orderStatus)
        {
            var orders = await _orderService.GetOrdersAsync(orderStatus);
            return IStatusCode(
                new ReturnAPI<IEnumerable<OrderDTO>>(orders)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO order)
        {
            await _orderService.CreateAsync(order);

            return IStatusCode(new ReturnAPI(HttpStatusCode.Created));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromQuery] OrderStatus? orderStatus)
        {
            await _orderService.UpdateOrderStatusAsync(id, orderStatus.Value);
            return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(Guid id)
        {
            await _orderService.RemoveAsync(id);
            return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
        }
    }
}
