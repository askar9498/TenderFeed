using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Tender.Crawler.ExternalServices;

public class TenderCrawler(IDbContextFactory<ApplicationDbContext> dbContextFactory) : ITenderCrawler
{
    public async Task<TenderModel> CrawlAsync(int pageNumber, int pageSize)
    {
        string url = $"https://gw.setadiran.ir/api/centralboard/cards/?NUMBER=number&TITLE=title&BOARDCODE=boardCode&BASEPRICE=basePrice&ORGNAME=orgName&SCORE=score&ONPERFORMING=onPerforming&NEWERINSERTDATE=newerInsertDate&OLDERINSERTDATE=olderInsertDate&NEWERJALALISENDDEADLINEDATE=newerJalaliSendDeadLineDate&OLDERJALALISENDDEADLINEDATE=olderJalaliSendDeadLineDate&CITYNAME=cityName&INSERTDATE=insertDate&BOARDNAME=boardName&PROVINCENAME=provinceName&JALALISENDDEADLINEDATE=jalaliSendDeadlineDate&JALALILASTEDITDATE=jalaliLastEditDate&JALALIDOCUMENTDEADLINEDATE=jalaliDocumentDeadlineDate&TAGCODE=tagCode&NEEDTYPE=needType&REQID=reqId&TABLEID=tableId&JALALIFROMPROPOSALDATE=jalaliFromProposalDate&PARTYNUMBER=partyNumber&PROVINCEANDCITY=provinceAndCity&ORGID=orgId&CATEGORYID=categoryId&CATEGORYNAME=categoryName&KEYWORD=keyword&FROM=from&SORT=sort&SEARCH_TYPE_CODE=searchTypeCode&FROM_SEND_DEADLINE_DATE=fromSendDeadlineDate&TO_SEND_DEADLINE_DATE=toSendDeadlineDate&FROM_DOCUMENT_DEADLINE_DATE=fromDocumentDeadlineDate&TO_DOCUMENT_DEADLINE_DATE=toDocumentDeadlineDate&FROM_PRICE=fromPrice&TO_PRICE=toPrice&SELECTED_ORGANIZATION=selectedOrganization&SELECTED_CATEGORY=selectedCategory&CLASSIFICATIONID=classificationId&APP_TREE=appTree&CHIP_CONTAINER_OPTIONS=chipContainerOptions&ID_TO_SELECTED_TREE_MAP=idToSelectedTreeMap&queryText=&pageNumber={pageNumber}&pageSize={pageSize}&sort=score,desc";
        using HttpClient client = new();
        string jsonResponse = await client.GetStringAsync(url);
        TenderModel? apiResponse = JsonConvert.DeserializeObject<TenderModel>(jsonResponse);
        for (int i = 1; i < apiResponse?.TotalPages; i++)
        {
            url = $"https://gw.setadiran.ir/api/centralboard/cards/?NUMBER=number&TITLE=title&BOARDCODE=boardCode&BASEPRICE=basePrice&ORGNAME=orgName&SCORE=score&ONPERFORMING=onPerforming&NEWERINSERTDATE=newerInsertDate&OLDERINSERTDATE=olderInsertDate&NEWERJALALISENDDEADLINEDATE=newerJalaliSendDeadLineDate&OLDERJALALISENDDEADLINEDATE=olderJalaliSendDeadLineDate&CITYNAME=cityName&INSERTDATE=insertDate&BOARDNAME=boardName&PROVINCENAME=provinceName&JALALISENDDEADLINEDATE=jalaliSendDeadlineDate&JALALILASTEDITDATE=jalaliLastEditDate&JALALIDOCUMENTDEADLINEDATE=jalaliDocumentDeadlineDate&TAGCODE=tagCode&NEEDTYPE=needType&REQID=reqId&TABLEID=tableId&JALALIFROMPROPOSALDATE=jalaliFromProposalDate&PARTYNUMBER=partyNumber&PROVINCEANDCITY=provinceAndCity&ORGID=orgId&CATEGORYID=categoryId&CATEGORYNAME=categoryName&KEYWORD=keyword&FROM=from&SORT=sort&SEARCH_TYPE_CODE=searchTypeCode&FROM_SEND_DEADLINE_DATE=fromSendDeadlineDate&TO_SEND_DEADLINE_DATE=toSendDeadlineDate&FROM_DOCUMENT_DEADLINE_DATE=fromDocumentDeadlineDate&TO_DOCUMENT_DEADLINE_DATE=toDocumentDeadlineDate&FROM_PRICE=fromPrice&TO_PRICE=toPrice&SELECTED_ORGANIZATION=selectedOrganization&SELECTED_CATEGORY=selectedCategory&CLASSIFICATIONID=classificationId&APP_TREE=appTree&CHIP_CONTAINER_OPTIONS=chipContainerOptions&ID_TO_SELECTED_TREE_MAP=idToSelectedTreeMap&queryText=&pageNumber={i}&pageSize={pageSize}&sort=score,desc";
            jsonResponse = await client.GetStringAsync(url);
            apiResponse = JsonConvert.DeserializeObject<TenderModel>(jsonResponse);
            foreach (Card card in apiResponse?.Content!)
            {
                try
                {
                    ApplicationDbContext context = dbContextFactory.CreateDbContext();
                    var newCard = await context.Cards.SingleOrDefaultAsync(x => x.Number == card.Number!);
                    if (newCard == null)
                    {
                        await context.AddAsync(card, CancellationToken.None);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            await Task.Delay(1000);
        }
        return apiResponse!;
    }
}