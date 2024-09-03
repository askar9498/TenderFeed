using Newtonsoft.Json;

namespace Tender.Crawler.ExternalServices;

public class TenderCrawler : ITenderCrawler
{
    public async Task<TenderModel> CrawlAsync()
    {
        string url = "https://gw.setadiran.ir/api/centralboard/cards/?NUMBER=number&TITLE=title&..."; // Replace with your full URL
        using HttpClient client = new();
        string jsonResponse = await client.GetStringAsync(url);
        TenderModel? apiResponse = JsonConvert.DeserializeObject<TenderModel>(jsonResponse);
        return apiResponse;
    }
}