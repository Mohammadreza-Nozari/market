﻿using System.ComponentModel.DataAnnotations;

namespace AuctionService.Contract.Request
{
    public class UpdateAuctionRequestContract
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public int? Mileage { get; set; }
        public string ImageUrl { get; set; }

    }
}
