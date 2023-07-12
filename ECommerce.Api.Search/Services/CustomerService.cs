using ECommerce.Api.Search.Interfaces;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                var client = this.httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customer/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var customer = JsonSerializer.Deserialize<dynamic>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return (true, customer, "Success");
                }
                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }

        }
    }
}
