using AuctionApp.API.Communication.Request;
using AuctionApp.API.Contracts;
using AuctionApp.API.Entities;
using AuctionApp.API.Services;
using AuctionApp.API.UseCases.Offers.CreateOffer;
using Bogus;
using FluentAssertions;
using Moq;

namespace UseCases.Test.Offer.CreateOffer
{
    public class CreateOfferUseCaseTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Sucess(int itemId)
        {
            var request = new Faker<RequestCreateOfferJson>()
                .RuleFor(request => request.Price, f => f.Random.Decimal(1, 3000)).Generate();


            var offerRepository = new Mock<IOfferRepository>();
            var loggedUser = new Mock<ILoggedUser>();
            loggedUser.Setup(x => x.User()).Returns(new User());

            var useCase = new CreateOfferUseCase(loggedUser.Object, offerRepository.Object);

            var act = () => useCase.Execute(itemId, request);

            act.Should().NotThrow();
        }
    }
}
