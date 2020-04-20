using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class InvitesListController : ControllerBase
    {
        private InvitesList invitesList;

        public InvitesListController(InvitesList invitesList)
        {
            if (invitesList == null)
                throw new ArgumentNullException(nameof(invitesList));

            this.invitesList = invitesList;
        }

        [HttpGet("{id}/invites")]
        public async Task<IActionResult> Get(int id)
        {
            var invites = await invitesList.Get(id);
            return Ok(invites);
        }
    }
}