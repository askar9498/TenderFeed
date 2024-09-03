using Microsoft.AspNetCore.Mvc;
using Tender.Crawler.ExternalServices;

namespace Tender.Crawler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrawlTendersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CrawlTendersController> _logger;
        private readonly ITenderCrawler _trendCrawler;

        public CrawlTendersController(ILogger<CrawlTendersController> logger, ITenderCrawler trendCrawler)
        {
            _logger = logger;
            _trendCrawler = trendCrawler;
        }

        [HttpGet(Name = "CrawlTenders")]
        public async Task<List<Card>> Crawl()
        {
            TenderModel news = await _trendCrawler.CrawlAsync();
            return news.Content;
        }
    }
}