using Domain.Dtos;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationServices _authentication;
        public AuthenticationController
        (
            IAuthenticationServices authentication
        )
        {
            _authentication = authentication;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if(!ModelState.IsValid)
                return BadRequest(
                    new ResponseDto()
                    {
                        Success = false,
                        Message = "The model is not valid",
                        content = ModelState
                    });
            var result = _authentication.Login(request);
            if (result.StartsWith("404")) 
                return NotFound(
                    new ResponseDto() {
                        Success = true,
                        Message = "The UserName or the Password is incorrect",
                        content = result
                    });
            if (result.StartsWith("500")) 
                return BadRequest(
                    new ResponseDto()
                    {
                        Success = false,
                        Message = "An error has been ocurred",
                        content = result
                    });
            return Ok(new ResponseDto()
                    {
                        Success = true,
                        Message = "Operation executed correctly",
                        content = result
                    });
        }
    }
}
