﻿using AuctionService.Database.Enums;

namespace AuctionService.Database.Entities
{
    public class AuctionEntity
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
        public ItemEntity Item { get; set; }
    }
}
