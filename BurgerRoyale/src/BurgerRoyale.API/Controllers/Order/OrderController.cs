using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Get a list of orders", Description = "Retrieves a list of orders based on the status.")]
        [ProducesResponseType(typeof(IEnumerable<ReturnAPI<OrderDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetOrders([FromQuery] OrderStatus? orderStatus)
        {
            var orders = await _orderService.GetOrdersAsync(orderStatus);
            return IStatusCode(
                new ReturnAPI<IEnumerable<OrderDTO>>(orders)
            );
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new order", Description = "Creates a new order.")]
        [ProducesResponseType(typeof(ReturnAPI<HttpStatusCode>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO order)
        {
            var orderNumber = await _orderService.CreateAsync(order);

            return IStatusCode(new ReturnAPI<string>(HttpStatusCode.Created, $"Order number: {orderNumber}"));
        }

        [HttpPut("{id:Guid}")]
        [SwaggerOperation(Summary = "Update an order", Description = "Updates an existing order by its ID.")]
        [ProducesResponseType(typeof(ReturnAPI<HttpStatusCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromQuery] OrderStatus? orderStatus)
        {
            await _orderService.UpdateOrderStatusAsync(id, orderStatus.Value);
            return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
        }

        [HttpDelete("{id:Guid}")]
        [SwaggerOperation(Summary = "Delete an order by ID", Description = "Deletes an order by its ID.")]
        [ProducesResponseType(typeof(ReturnAPI<HttpStatusCode>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrderAsync(Guid id)
        {
            await _orderService.RemoveAsync(id);
            return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
        }
    }
}