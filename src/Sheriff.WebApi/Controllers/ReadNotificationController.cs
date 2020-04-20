using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("notifications")]
    public class ReadNotificationController : ControllerBase
    {
        private ReadNotification readNotification;

        public ReadNotificationController(ReadNotification readNotification)
        {
            if (readNotification == null)
                throw new ArgumentNullException(nameof(readNotification));

            this.readNotification = readNotification;
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> Post(int id)
        {
            var notif = await readNotification.Read(id);
            return Ok(notif);
        }
    }
}