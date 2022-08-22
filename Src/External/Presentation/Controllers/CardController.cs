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

        /// <summary>
        /// Returns a list of Card Created by the User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetCards()
        {
            try
            {
                var Id = JwtServices.GetClaimUser(User);
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
                if (response == null)
                    return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "404 not found any Card",
                            content = response
                        });
                return Ok(
                        new ResponseDto()
                        {
                            Success = true,
                            Message = "Operation executed correctly",
                            content = response
                        });
            }
            catch (Exception ex)
            {
                return BadRequest(
                       new ResponseDto()
                       {
                           Success = false,
                           Message = "Failed the execution",
                           content = ex.Message
                       });
            }
        }

        /// <summary>
        /// Create a new Card with a Balance
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CardRequestDto requests)
        {
            try
            {
                var Id = JwtServices.GetClaimUser(User);
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

                if (response == null)
                    return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "Failed the execution",
                            content = response
                        });

                return Ok(
                        new ResponseDto()
                        {
                            Success = true,
                            Message = "Operation executed correctly",
                            content = response
                        });
            }
            catch (Exception ex)
            {
                return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "Failed the execution",
                            content = ex.Message
                        });
            }
        }

        /// <summary>
        /// Returns a Card Balance based on the Card Number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{Number}")]
        public IActionResult GetCardBalance([FromRoute] string Number)
        {
            try
            {
                var Id = JwtServices.GetClaimUser(User);
                if (Id.StartsWith("Error"))
                    return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "The Jwt is Invalid",
                            content = null
                        });
                var UserId = Convert.ToInt32(Id);

                var response = _Card.GetCardBalance(Number, UserId);


                if (response == null)
                    return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "404 not found any Card",
                            content = response
                        });
                return Ok(
                        new ResponseDto()
                        {
                            Success = true,
                            Message = "Operation executed correctly",
                            content = response
                        });
            }
            catch (Exception ex)
            {
                return BadRequest(
                       new ResponseDto()
                       {
                           Success = false,
                           Message = "Failed the execution",
                           content = ex.Message
                       });
            }
        }

        /// <summary>
        /// Returns a Card Balance based on the Card Number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{Number}/Details")]
        public IActionResult GetDetailBalance([FromRoute] string Number)
        {
            try
            {
                var Id = JwtServices.GetClaimUser(User);
                if (Id.StartsWith("Error"))
                    return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "The Jwt is Invalid",
                            content = null
                        });
                var UserId = Convert.ToInt32(Id);

                var response = _Card.GetBalanceDetail(Number, UserId);


                if (response == null)
                    return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "404 not found any Card",
                            content = response
                        });
                return Ok(
                        new ResponseDto()
                        {
                            Success = true,
                            Message = "Operation executed correctly",
                            content = response
                        });
            }
            catch (Exception ex)
            {
                return BadRequest(
                        new ResponseDto()
                        {
                            Success = false,
                            Message = "Failed the execution",
                            content = ex.Message
                        });
            }
        }
    }
}
