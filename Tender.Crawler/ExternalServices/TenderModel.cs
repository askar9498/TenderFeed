namespace Tender.Crawler.ExternalServices;

public class TenderModel
{
    public List<Card> Content { get; set; }
    public int TotalPages { get; set; }
    public int TotalElements { get; set; }
    public object ResultsExtraInfo { get; set; }
}
