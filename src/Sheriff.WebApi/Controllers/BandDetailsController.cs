using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bands")]
    public class BandDetailsController : ControllerBase
    {
        private BandDetails bandDetails;

        public BandDetailsController(BandDetails bandDetails)
        {
            if (bandDetails == null)
                throw new ArgumentNullException(nameof(bandDetails));

            this.bandDetails = bandDetails;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var band = await bandDetails.Get(id);
            return Ok(band);
        }
    }
}