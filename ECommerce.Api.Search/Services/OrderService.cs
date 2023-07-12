using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var jsonOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resutl = JsonSerializer.Deserialize<IEnumerable<Order>>(content, jsonOption);
                    return (true, resutl, "Success");
                }
                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.ToString());
                throw;
            }
        }
    }
}
