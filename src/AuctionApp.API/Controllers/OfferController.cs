using AuctionApp.API.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.API.Controllers
{
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
