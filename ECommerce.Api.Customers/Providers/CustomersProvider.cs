using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomerDbContext customerDbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomerDbContext customerDbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.customerDbContext = customerDbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!customerDbContext.Customers.Any())
            {
                customerDbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Asim", Address = "Lahore, Pakistan" });
                customerDbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Ali", Address = "Sialkot, Pakistan" });
                customerDbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Ahsan", Address = "Karachi, Pakistan" });
                customerDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int customerId)
        {
            try
            {
                logger?.LogInformation("Quering Order");
                var customer = await customerDbContext.Customers.FirstOrDefaultAsync(p => p.Id == customerId);
                if (customer != null)
                {
                    logger?.LogInformation("Order Found in the database");
                    var result = mapper.Map<Models.Customer>(customer);
                    return (true, result, "Success");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }

        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Quering Order");
                var customers = await customerDbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"Total {customers.Count} Customer Found in the database");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, "Success");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
