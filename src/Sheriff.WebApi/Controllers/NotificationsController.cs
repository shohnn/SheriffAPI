using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("bandits")]
    public class NotificationsController : ControllerBase
    {
        private AppNotifications notifications;

        public NotificationsController(AppNotifications notifications)
        {
            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            this.notifications = notifications;
        }

        [HttpGet("{id}/notifications")]
        public async Task<IActionResult> Get(int id)
        {
            var notifs = await notifications.Get(id);
            return Ok(notifs);
        }
    }
}