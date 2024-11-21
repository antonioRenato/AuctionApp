using AuctionApp.API.Communication.Request;
using AuctionApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.API.Controllers
{
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
    public class OfferController : BaseController
    {
        [HttpPost]
        [Route("{itemId}")]
        public IActionResult CreateOffer([FromRoute]int itemId, [FromBody] RequestCreateOfferJson request)
        {
            return Created();
        }
    }
}
