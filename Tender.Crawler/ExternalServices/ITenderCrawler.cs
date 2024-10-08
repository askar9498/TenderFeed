namespace Tender.Crawler.ExternalServices;

public interface ITenderCrawler
{
    Task<TenderModel> CrawlAsync(int pageNumber, int pageSize);
}