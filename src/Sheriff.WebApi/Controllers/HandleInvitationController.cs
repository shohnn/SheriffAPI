using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("invites")]
    public class HandleInvitationController : ControllerBase
    {
        private HandleInvitation handleInvite;

        public HandleInvitationController(HandleInvitation handleInvite)
        {
            if (handleInvite == null)
                throw new ArgumentNullException(nameof(handleInvite));

            this.handleInvite = handleInvite;
        }

        [HttpPost("handle")]
        public async Task<IActionResult> Post(DTOs.HandleInvitation request)
        {
            var invite = await handleInvite.Handle(request);
            return Ok(invite);
        }
    }
}