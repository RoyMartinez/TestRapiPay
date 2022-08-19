using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.Dtos;
using Application.Services;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly IRecordsServices _Records;
        public TransactionController
        (
            IRecordsServices Records
        )
        {
            _Records = Records;
        }

        [HttpPost]
        [Authorize]
        public  async Task<IActionResult> Create([FromBody] RecordRequestDto requests)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
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

                var response = await _Records.CreateRecord(requests, UserId);

                if (!response.StartsWith("200"))
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
    }
}
