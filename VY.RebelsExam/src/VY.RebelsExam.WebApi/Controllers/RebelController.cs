using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VY.RebelsExam.Business.Contracts.Services;
using VY.RebelsExam.Dtos.Domain.V1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VY.RebelsExam.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RebelController : ControllerBase
    {
        private readonly IRebelService _rebelService;
        public RebelController(IRebelService rebelService)
        {
            _rebelService = rebelService;
        }


        // POST api/<RebelController>
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(int), 500)]
        [HttpPost("RegisterRebels")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<RebelDto> rebel)
        {
            var res = await _rebelService.AddRebel(rebel);
            if (res.HasErrors())
            {
                if (res.HasExceptions())
                {
                    return StatusCode(500);
                }
                else return BadRequest(res.Errors);
            }
            return Ok(res.Result);
        }

    }
}
