using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.API.Controllers
{
    public class OfferController : BaseController
    {
        [HttpPost]
        [Route("{itemId}")]
        public IActionResult CreateOffer([FromRoute]int itemId)
        {
            return Created();
        }
    }
}
