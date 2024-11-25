using AuctionApp.API.Communication.Request;
using AuctionApp.API.Entities;
using AuctionApp.API.Repositories;
using AuctionApp.API.Services;

namespace AuctionApp.API.UseCases.Offers.CreateOffer
{
    public class CreateOfferUseCase
    {
        private readonly LoggedUser _loggedUser;
        
        public CreateOfferUseCase(LoggedUser loggedUser)
        {
            _loggedUser = loggedUser;
        }

        public void Execute(int itemId, RequestCreateOfferJson request)
        {
            var repository = new AuctionAppDbContext();

            var user = _loggedUser.User();

            var offer = new Offer
            {
                CreatedOn = DateTime.Now,
                ItemId = itemId,
                Price = request.Price,
                UserId = user.Id,
            };

            repository.Offers.Add(offer);
        }
    }
}
