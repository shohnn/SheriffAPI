using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class JoinAppController : ControllerBase
    {
        private JoinApp joinApp;

        public JoinAppController(JoinApp joinApp)
        {
            if (joinApp == null)
                throw new ArgumentNullException(nameof(joinApp));

            this.joinApp = joinApp;
        }

        [HttpPost("invite")]
        public async Task<IActionResult> Post(DTOs.JoinApp request)
        {
            var notif = await joinApp.Invite(request);
            return Ok(notif);
        }
    }
}