using AuctionApp.API.Contracts;
using AuctionApp.API.Entities;

namespace AuctionApp.API.UseCases.Auctions.GetCurrent
{
    public class GetCurrentAuctionUseCase
    {
        private readonly IAuctionRepository _repository;

        public GetCurrentAuctionUseCase(IAuctionRepository repository)
        {
            _repository = repository;
        }

        public Auction? Execute()
        {
            return _repository.GetCurrent();
        }
    }
}
