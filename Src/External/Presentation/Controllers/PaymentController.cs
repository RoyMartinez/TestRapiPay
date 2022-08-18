using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {

        private readonly IRecordsServices _Records;
        public PaymentController
        (
            IRecordsServices Records
        )
        {
            _Records = Records;
        }
        [Authorize]
        public IActionResult Index()
        {
            return Ok("This is Payment Controller");
        }
    }
}
