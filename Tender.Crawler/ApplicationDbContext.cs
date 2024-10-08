using Microsoft.EntityFrameworkCore;
using Tender.Crawler.ExternalServices;

namespace Tender.Crawler;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define your DbSets here
    public DbSet<Card> Cards { get; set; }
}