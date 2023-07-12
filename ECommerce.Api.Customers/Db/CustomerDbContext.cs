using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Db
{
    public class CustomerDbContext : DbContext
    {
        private readonly DbContextOptions dbContextOptions;

        public DbSet<Db.Customer> Customers { get; set; }
        public CustomerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }
    }
}
