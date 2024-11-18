using AuctionApp.API.Entities;
using AuctionApp.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.API.UseCases.Auctions.GetCurrent
{
    public class GetCurrentAuctionUseCase
    {
        public Auction? Execute()
        {
            var repository = new AuctionAppDbContext();
            
            return repository.Auctions.Include(x => x.Items).FirstOrDefault(data => data.Starts >= DateTime.Now && data.Ends <= DateTime.Now);
        }
    }
}
