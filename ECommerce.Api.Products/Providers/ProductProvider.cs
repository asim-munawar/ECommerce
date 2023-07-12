using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProvider : IProductsProvider
    {
        private readonly ProductsDbContext productsDbContext;
        private readonly ILogger<ProductProvider> logger;
        private readonly IMapper mapper;

        public ProductProvider(ProductsDbContext productsDbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            this.productsDbContext = productsDbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!this.productsDbContext.Products.Any())
            {
                this.productsDbContext.Products.Add(new Db.Product { Id = 1, Name = "Mouse", Price = 50, Inventory = 100 });
                this.productsDbContext.Products.Add(new Db.Product { Id = 2, Name = "Keyboard", Price = 100, Inventory = 200 });
                this.productsDbContext.Products.Add(new Db.Product { Id = 3, Name = "Monitor", Price = 300, Inventory = 75 });
                this.productsDbContext.Products.Add(new Db.Product { Id = 4, Name = "CPU", Price = 200, Inventory = 150 });
                this.productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await this.productsDbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, "Success");
                }
                return (false, null, "Not Found");


            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (IsSuccess: false, null, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await this.productsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, "Success");
                }
                return (false, null, "Not Found");


            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
