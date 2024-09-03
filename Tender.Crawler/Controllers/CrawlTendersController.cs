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
    public async Task<TenderModel> Crawl(int pageNumber = 0, int pageSize = 5)
    {
        TenderModel news = await _trendCrawler.CrawlAsync(pageNumber, pageSize);
        return news;
    }
}