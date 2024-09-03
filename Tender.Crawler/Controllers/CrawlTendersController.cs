using Microsoft.AspNetCore.Mvc;
using Tender.Crawler.ExternalServices;

namespace Tender.Crawler.Controllers;

[ApiController]
[Route("[controller]")]
public class CrawlTendersController : ControllerBase
{
    private readonly ITenderCrawler _trendCrawler;

    public CrawlTendersController(ITenderCrawler trendCrawler)
    {
        _trendCrawler = trendCrawler;
    }

    [HttpGet(Name = "CrawlTenders")]
    public async Task<List<Card>> Crawl(int pageNumber, int pageSize)
    {
        TenderModel news = await _trendCrawler.CrawlAsync(pageNumber, pageSize);
        return news.Content;
    }
}