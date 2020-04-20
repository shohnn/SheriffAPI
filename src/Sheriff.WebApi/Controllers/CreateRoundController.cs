using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("rounds")]
    public class CreateRoundController : ControllerBase
    {
        private CreateRound createRound;

        public CreateRoundController(CreateRound createRound)
        {
            if (createRound == null)
                throw new ArgumentNullException(nameof(createRound));

            this.createRound = createRound;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DTOs.CreateRound request)
        {
            var round = await createRound.Create(request);
            return Ok(round);
        }
    }
}