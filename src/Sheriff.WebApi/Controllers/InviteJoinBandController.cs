using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bands")]
    public class InviteJoinBandController : ControllerBase
    {
        private InviteJoinBand inviteJoin;

        public InviteJoinBandController(InviteJoinBand inviteJoin)
        {
            if (inviteJoin == null)
                throw new ArgumentNullException(nameof(inviteJoin));

            this.inviteJoin = inviteJoin;
        }

        [HttpPost("invite")]
        public async Task<IActionResult> Post(DTOs.InviteJoinBand request)
        {
            var notif = await inviteJoin.Invite(request);
            return Ok(notif);
        }
    }
}