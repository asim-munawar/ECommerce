using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var resutl = await this.orderProvider.GetOrdersAsync();
            if (resutl.IsSuccess)
            {
                return Ok(resutl.orders);
            }
            return NotFound();

        }
        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetOrder(int CustomerId)
        {
            var resutl = await this.orderProvider.GetOrderAsync(CustomerId);
            if (resutl.IsSuccess)
            {
                return Ok(resutl.orders);
            }
            return NotFound();

        }
    }
}
