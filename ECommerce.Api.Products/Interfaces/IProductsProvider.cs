namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductAsync(int id);
    }
}
