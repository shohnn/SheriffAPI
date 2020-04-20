using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class BanditBandsController : ControllerBase
    {
        private BanditBandsList bandsList;

        public BanditBandsController(BanditBandsList bandsList)
        {
            if (bandsList == null)
                throw new ArgumentNullException(nameof(bandsList));

            this.bandsList = bandsList;
        }

        [HttpGet("{banditId}/bands")]
        public async Task<IActionResult> Get(int banditId)
        {
            var bands = await bandsList.Get(banditId);
            return Ok(bands);
        }
    }
}