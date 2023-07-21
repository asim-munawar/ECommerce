using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var option = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(option);
            CreateDummyProducts(dbContext);

            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductProvider(dbContext, null, mapper);


            var result = await productsProvider.GetProductsAsync();

            Assert.True(result.IsSuccess);
            Assert.True(result.products.Any());
            Assert.Equal("Success", result.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var option = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(option);
            CreateDummyProducts(dbContext);

            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductProvider(dbContext, null, mapper);


            var result = await productsProvider.GetProductAsync(1);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.product);
            Assert.True(result.product.Id == 1);
            Assert.Equal("Success", result.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidId()
        {
            var option = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInvalidId))
                .Options;
            var dbContext = new ProductsDbContext(option);
            CreateDummyProducts(dbContext);

            var productprofile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductProvider(dbContext, null, mapper);


            var result = await productsProvider.GetProductAsync(-1);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.product);
            Assert.Null(result.ErrorMessage);
        }

        private void CreateDummyProducts(ProductsDbContext dbContext)
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var product = new Product { Id = i, Name = i.ToString(), Price = (decimal)(i * 3.14), Inventory = i + 10 };
                    dbContext.Products.Add(product);
                }
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}