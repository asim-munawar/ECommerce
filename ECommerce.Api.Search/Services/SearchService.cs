using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService,
            IProductService productService,
            ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int CustomerId)
        {
            var CustomerResult = await customerService.GetCustomerAsync(CustomerId);
            var OrderResult = await orderService.GetOrdersAsync(CustomerId);
            var ProductResult = await productService.GetProductsAsync();
            if (OrderResult.IsSuccess)
            {
                foreach (var order in OrderResult.Orders)
                {
                    foreach (var orderItem in order.Items)
                    {
                        orderItem.ProductName = ProductResult.IsSuccess ?
                            ProductResult.Products.FirstOrDefault(p => p.Id == orderItem.ProductId).Name :
                            "Product Name is not available";
                    }
                }

                var Order = new
                {
                    Customer = CustomerResult.IsSuccess ? CustomerResult.Customer : new { Name = "Customer Information is not found" },
                    Orders = OrderResult.Orders,
                };
                return (OrderResult.IsSuccess, Order);
            }
            return (OrderResult.IsSuccess, null);
        }
    }
}
