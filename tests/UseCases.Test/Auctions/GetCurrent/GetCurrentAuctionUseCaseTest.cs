﻿using AuctionApp.API.Contracts;
using AuctionApp.API.UseCases.Auctions.GetCurrent;
using FluentAssertions;
using Moq;
using AuctionApp.API.Entities;
using Bogus;
using Bogus.DataSets;
using AuctionApp.API.Enums;

namespace UseCases.Test.Auctions.GetCurrent
{
    public class GetCurrentAuctionUseCaseTest
    {
        [Fact]
        public void Sucess()
        {
            var entity = new Faker<Auction>()
                .RuleFor(auction => auction.Id, f => f.Random.Number(1, 3000))
                .RuleFor(auction => auction.Name, f => f.Lorem.Word())
                .RuleFor(auction => auction.Starts, f => f.Date.Past())
                .RuleFor(auction => auction.Ends, f => f.Date.Future())
                .RuleFor(auction => auction.Items, (f, auction) => new List<Item>
                {
                    new Item
                    {
                        Id = f.Random.Number(1, 700),
                        Name = f.Commerce.ProductName(),
                        Brand = f.Commerce.Department(),
                        BasePrice = f.Random.Decimal(50, 1000),
                        Condition = f.PickRandom<Condition>(),
                        AuctionId = auction.Id,
                    }
                }).Generate();


            var mock = new Mock<IAuctionRepository>();
            mock.Setup(i => i.GetCurrent()).Returns(entity);

            var useCase = new GetCurrentAuctionUseCase(mock.Object);

            var auction = useCase.Execute();

            auction.Should().NotBeNull();
            auction.Id.Should().Be(entity.Id);
            auction.Name.Should().Be(entity.Name);    
        }
    }
}
