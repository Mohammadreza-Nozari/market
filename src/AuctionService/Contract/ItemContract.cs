using AuctionService.Database.Entities;
using System.Text.Json.Serialization;

namespace AuctionService.Contract
{
    public class ItemContract
    {
        public Guid Id { get; set; }
        public Guid AuctionId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public AuctionContract Auction { get; set; }
    }
}
