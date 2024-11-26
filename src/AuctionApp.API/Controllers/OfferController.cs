using AuctionApp.API.Communication.Request;
using AuctionApp.API.Filters;
using AuctionApp.API.UseCases.Offers.CreateOffer;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.API.Controllers
{
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
    public class OfferController : BaseController
    {
        [HttpPost]
        [Route("{itemId}")]
        public IActionResult CreateOffer([FromRoute]int itemId, [FromBody] RequestCreateOfferJson request, [FromServices] CreateOfferUseCase useCase)
        {
            var id = useCase.Execute(itemId, request);

            return Created(string.Empty, id);
        }
    }
}
