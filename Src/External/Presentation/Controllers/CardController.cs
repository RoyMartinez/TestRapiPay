using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : Controller
    {

        private readonly ICardsServices _Card;
        public CardController
        (
            ICardsServices Card
        )
        {
            _Card = Card;
        }
        [Authorize]
        public IActionResult Index()
        {
            return Ok("This is card Controller");
        }
    }
}
