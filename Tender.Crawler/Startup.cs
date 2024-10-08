using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tender.Crawler;

public class Startup(IConfiguration configuration, IHostEnvironment environment)
{
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = configuration.GetConnectionString("TenderRawData")!;
        services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        logger.LogInformation("CodalCrawler Startup completed");
    }

    private static void ConfigureRouting(IApplicationBuilder app)
    {
        app.UseRouting();
    }
}