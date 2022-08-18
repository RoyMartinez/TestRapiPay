using Application.Services;
using Domain.Dtos;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetCards()
        {
            var Id = Jwt.GetClaimUser(User);
            if (Id.StartsWith("Error"))
                return BadRequest(
                    new ResponseDto()
                    {
                        Success = false,
                        Message = "The Jwt is Invalid",
                        content = null
                    });
            var UserId = Convert.ToInt32(Id);
            var response = _Card.GetCards(UserId);
            return Ok(
                    new ResponseDto()
                    {
                        Success = true,
                        Message = "Operation executed correctly",
                        content = response
                    });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CardRequestDto requests)
        {
            var Id = Jwt.GetClaimUser(User);
            if (Id.StartsWith("Error"))
                return BadRequest(
                    new ResponseDto()
                    {
                        Success = false,
                        Message = "The Jwt is Invalid",
                        content = null
                    });
            var UserId = Convert.ToInt32(Id);
            var response = _Card.CreateCard(requests, UserId);
            return Ok(
                    new ResponseDto()
                    {
                        Success = true,
                        Message = "Operation executed correctly",
                        content = response
                    });
        }
    }
}
