using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Contract;
using SearchService.Database.Document;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController:ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ItemDocument>>> SearchItems([FromQuery]SearchRequestContract searchRequest)
        {

            var query = DB.PagedSearch<ItemDocument>();

            if(!string.IsNullOrEmpty(searchRequest.SearchTerm))
            {
                query.Match(Search.Full, searchRequest.SearchTerm).SortByTextScore();
            }


            query = searchRequest.OrderBy switch
            {
                "make" => (PagedSearch<ItemDocument>)query.Sort(x => x.Ascending(a => a.Make)),
                "new" => (PagedSearch<ItemDocument>)query.Sort(x => x.Descending(a => a.Make)),
                _=> (PagedSearch<ItemDocument>)query.Sort(x => x.Ascending(a => a.AuctionEnd)),
            };

            query = searchRequest.FilterBy switch
            {
                "finished" => (PagedSearch<ItemDocument>)query.Match(x=>x.AuctionEnd< DateTime.UtcNow),
                "endingSoon" => (PagedSearch<ItemDocument>)query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6)),
                _ => (PagedSearch<ItemDocument>)query.Match(x => x.AuctionEnd > DateTime.UtcNow.AddHours(6)),
            };

            query.PageNumber(searchRequest.PageNumber);
            query.PageSize(searchRequest.PageSize);

            query.Sort(item => item.Ascending(x => x.Make));

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });

        }

    }
}
