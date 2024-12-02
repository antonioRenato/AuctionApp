using AuctionApp.API.Contracts;
using AuctionApp.API.UseCases.Auctions.GetCurrent;
using FluentAssertions;
using Moq;
using AuctionApp.API.Entities;
using Bogus;

namespace UseCases.Test.Auctions.GetCurrent
{
    public class GetCurrentAuctionUseCaseTest
    {
        [Fact]
        public void Sucess()
        {
            var entity = new Faker<Auction>()
                .RuleFor(x => x.Id, f => f.Random.Number(1, 3000))
                .RuleFor(x => x.Name, f => f.Lorem.Word())
                .RuleFor(x => x.Starts, f => f.Date.Past())
                .RuleFor(x => x.Ends, f => f.Date.Future())
                .RuleFor(x => x.Items, f => new List<Item>
                {
                    new Item
                    {

                    }
                });


            var mock = new Mock<IAuctionRepository>();
            mock.Setup(i => i.GetCurrent()).Returns(entity);

            var useCase = new GetCurrentAuctionUseCase(mock.Object);

            var auction = useCase.Execute();

            auction.Should().NotBeNull();
        }
    }
}
