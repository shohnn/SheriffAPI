using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bands")]
    public class CreateBandController : ControllerBase
    {
        private CreateBand createBand;

        public CreateBandController(CreateBand createBand)
        {
            if (createBand == null)
                throw new ArgumentNullException(nameof(createBand));

            this.createBand = createBand;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DTOs.CreateBand request)
        {
            var band = await createBand.Create(request);
            return Ok(band);
        }
    }
}