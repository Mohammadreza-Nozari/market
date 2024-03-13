using AuctionService.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
         
        public DbSet<AuctionEntity> Auctions { get; set; }
        public DbSet<ItemEntity> Items { get; set; }

    }
}
