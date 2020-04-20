using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class BanditDetailsController : ControllerBase
    {
        private BanditDetails banditDetails;

        public BanditDetailsController(BanditDetails banditDetails)
        {
            if (banditDetails == null)
                throw new ArgumentNullException(nameof(banditDetails));

            this.banditDetails = banditDetails;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var bandit = await banditDetails.Get(id);
            return Ok(bandit);
        }
    }
}