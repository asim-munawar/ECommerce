namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        public Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int CustomerId);
    }
}
