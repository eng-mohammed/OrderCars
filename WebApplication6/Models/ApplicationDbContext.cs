using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Models
{
    // Data/ApplicationDbContext.cs
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<OrderCar> OrderCars { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }

}
