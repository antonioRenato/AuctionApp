using AuctionApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.Repositories
{
    public class AuctionAppDbContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }

        public AuctionAppDbContext(DbContextOptions options) : base(options) { }
    }
}
