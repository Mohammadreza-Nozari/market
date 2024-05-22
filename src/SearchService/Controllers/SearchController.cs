using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Database.Document;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController:ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<ItemDocument>>> SearchItems(string searchTerm,int pageNumber=1,int pageSize=4)
        {

            var query = DB.PagedSearch<ItemDocument>();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }

            query.PageNumber(pageNumber);
            query.PageSize(pageSize);

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
