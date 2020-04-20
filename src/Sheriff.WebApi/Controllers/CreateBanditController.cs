using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class CreateBanditController : ControllerBase
    {
        private CreateBandit createBandit;

        public CreateBanditController(CreateBandit createBandit)
        {
            if (createBandit == null)
                throw new ArgumentNullException(nameof(createBandit));

            this.createBandit = createBandit;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DTOs.CreateBandit request)
        {
            var bandit = await createBandit.Create(request);
            return Ok(bandit);
        }
    }
}