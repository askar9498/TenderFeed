namespace Tender.Crawler
{
    public class CrawlTenderModel
    {
        public long Number { get; set; }
        public string Title { get; set; }
        public int BoardCode { get; set; }
        public decimal? BasePrice { get; set; }
        public string OrgName { get; set; }
        public int Score { get; set; }
        public string CityName { get; set; }
        public DateTime? InsertDate { get; set; }
        public string BoardName { get; set; }
        public string ProvinceName { get; set; }
        public string JalaliSendDeadlineDate { get; set; }
        public DateTime? JalaliLastEditDate { get; set; }
        public DateTime? JalaliDocumentDeadlineDate { get; set; }
        public int TableId { get; set; }
        public int TagCode { get; set; }
        public string NeedType { get; set; }
        public string ReqId { get; set; }
        public DateTime? JalaliFromProposalDate { get; set; }
        public int? PartyNumber { get; set; }
        public int OrgId { get; set; }
        public string AucState { get; set; }
        public int CartId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? LocId { get; set; }
    }
}