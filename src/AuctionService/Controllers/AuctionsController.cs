using AuctionService.Contract;
using AuctionService.Database.Context;
using AuctionService.Database.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        public AuctionsController(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionContract>>> GetAllAuctions()
        {
            var auctions = await _context.Auctions.Include(x => x.Item).OrderBy(x => x.Item.Make).ToListAsync();

            return _mapper.Map<List<AuctionContract>>(auctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionContract>> GetAuctionById(Guid id)
        {
            var auction = await _context.Auctions.Include(x => x.Item).OrderBy(x => x.Item.Make).FirstAsync(dr => dr.Id == id);

            return auction == null ? NotFound() : _mapper.Map<AuctionContract>(auction);
        }
    }
}
