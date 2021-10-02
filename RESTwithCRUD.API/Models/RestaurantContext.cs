using Microsoft.EntityFrameworkCore;

namespace RESTwithCRUD.API.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasMany(c => c.Bookings)
                .WithOne(c => c.Restaurant)
                .HasForeignKey(s => s.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); ;

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
