namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrderAsync(int customerid);
    }
}
