using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Db.Order> Orders { get; set; }
        public OrderDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
