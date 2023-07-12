using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        private readonly DbContextOptions dbContextOptions;

        public DbSet<Db.Product> Products { get; set; }
        public ProductsDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }
    }
}
