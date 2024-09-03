using Microsoft.EntityFrameworkCore;

namespace Tender.Crawler;

internal class ApplicationDbContext : DbContext
{
    private readonly string _con = string.Empty;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext(string connectionString)
    {
        _con = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_con);
    }
}