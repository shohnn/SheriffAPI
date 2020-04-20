using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bands")]
    public class BandsListController : ControllerBase
    {
        private BandsList bandsList;

        public BandsListController(BandsList bandsList)
        {
            if (bandsList == null)
                throw new ArgumentNullException(nameof(bandsList));

            this.bandsList = bandsList;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            var bands = await bandsList.Get(name);
            return Ok(bands);
        }
    }
}