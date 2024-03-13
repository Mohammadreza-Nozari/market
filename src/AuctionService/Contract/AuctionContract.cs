using AuctionService.Database.Entities;
using AuctionService.Database.Enums;
using System.Text.Json.Serialization;

namespace AuctionService.Contract
{
    public class AuctionContract
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionEnd { get; set; }
        public StatusType Status { get; set; }
        [JsonIgnore]
        public ItemContract Item { get; set; }
    }
}
