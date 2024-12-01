using AuctionApp.API.Contracts;
using AuctionApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.Repositories.DataAccess
{
    public class AuctionRepository : IAuctionRepository
    {
        AuctionAppDbContext _dbContext;
        public AuctionRepository(AuctionAppDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        public Auction? GetCurrent()
        {
            var today = DateTime.Now;

            return _dbContext.Auctions.Include(x => x.Items).FirstOrDefault(data => data.Starts >= today && today <= data.Ends);
        }
    }
}
