namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        public Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
