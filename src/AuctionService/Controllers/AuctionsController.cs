using AuctionService.Database.Context;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _dbContext;
        public AuctionsController(DatabaseContext context,IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = context;
        }
    }
}
