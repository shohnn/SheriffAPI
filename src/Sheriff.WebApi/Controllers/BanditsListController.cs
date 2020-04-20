using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class BanditsListController : ControllerBase
    {
        private BanditsList banditsList;

        public BanditsListController(BanditsList banditsList)
        {
            if (banditsList == null)
                throw new ArgumentNullException(nameof(banditsList));

            this.banditsList = banditsList;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bandits = await banditsList.Get();
            return Ok(bandits);
        }
    }
}