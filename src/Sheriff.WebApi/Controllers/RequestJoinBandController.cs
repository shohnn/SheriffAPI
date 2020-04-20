using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bands")]
    public class RequestJoinBandController : ControllerBase
    {
        private RequestJoinBand requestJoin;

        public RequestJoinBandController(RequestJoinBand requestJoin)
        {
            if (requestJoin == null)
                throw new ArgumentNullException(nameof(requestJoin));

            this.requestJoin = requestJoin;
        }

        [HttpPost("request")]
        public async Task<IActionResult> Post(DTOs.RequestJoinBand request)
        {
            var notif = await requestJoin.Invite(request);
            return Ok(notif);
        }
    }
}