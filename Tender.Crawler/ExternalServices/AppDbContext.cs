using Microsoft.EntityFrameworkCore;

namespace Tender.Crawler.ExternalServices;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>().HasKey(c => c.Id); // Define primary key
        // You can add additional configurations here
    }
}