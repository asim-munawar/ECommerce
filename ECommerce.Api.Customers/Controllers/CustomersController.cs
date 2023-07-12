using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var resutl = await customersProvider.GetCustomersAsync();
            if (resutl.IsSuccess)
            {
                return Ok(resutl.Customers);
            }
            return NotFound();
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomersAsync(int Id)
        {
            var resutl = await customersProvider.GetCustomerAsync(Id);
            if (resutl.IsSuccess)
            {
                return Ok(resutl.Customer);
            }
            return NotFound();
        }
    }
}
