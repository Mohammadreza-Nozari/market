using AuctionService.Contract;
using AuctionService.Contract.Request;
using AuctionService.Database.Context;
using AuctionService.Database.Entities;
using AutoMapper;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly DatabaseContext _context;
        public AuctionsController(DatabaseContext context, IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            this._publishEndpoint = publishEndpoint;
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

        [HttpPost]
        public async Task<ActionResult<AuctionContract>> CreateAuction(CreateAuctionRequestContract createAuction)
        {
            var auction = _mapper.Map<AuctionEntity>(createAuction);
            // TODO: add current user as seller
            auction.Seller = "Test";
            await _context.Auctions.AddAsync(auction);

            var result = await _context.SaveChangesAsync() > 0;

            var newAuction = _mapper.Map<AuctionContract>(auction);

            await _publishEndpoint.Publish(_mapper.Map<AuctionCreatedContract>(newAuction));

            if (!result) return BadRequest("Could not save changes to the Database");

            return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionContract>(auction));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id,UpdateAuctionRequestContract updateAuction)
        {
            var auction = await _context.Auctions.Include(dr => dr.Item).FirstOrDefaultAsync(dr=>dr.Id == id);

            if (auction == null) return NotFound();

            //TODO: check seller == username

            auction.Item.Make = updateAuction.Make ?? auction.Item.Make;
            auction.Item.Model = updateAuction.Model ?? auction.Item.Model;
            auction.Item.Color = updateAuction.Color ?? auction.Item.Color;
            auction.Item.Mileage = updateAuction.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = updateAuction.Year ?? auction.Item.Year;
            auction.Item.ImageUrl = updateAuction.ImageUrl ?? auction.Item.ImageUrl;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Could not update to the Database");

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _context.Auctions.Include(dr => dr.Item).FirstOrDefaultAsync(dr => dr.Id == id);

            if (auction == null) return NotFound();

             _context.Auctions.Remove(auction);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Could not delete to the Database");

            return Ok();

        }

    }
}
