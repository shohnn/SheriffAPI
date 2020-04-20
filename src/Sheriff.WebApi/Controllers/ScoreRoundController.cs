using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sheriff.Application.UseCases;
using DTOs = Sheriff.Application.DTOs;

namespace Sheriff.WebApi.Controllers
{
    [ApiController]
    [Route("rounds")]
    public class NotifyScoreRoundController : ControllerBase
    {
        private ScoreRound scoreRound;
        private NotifyScoreRound notifyScoreRound;

        public NotifyScoreRoundController(ScoreRound scoreRound, NotifyScoreRound notifyScoreRound)
        {
            if (scoreRound == null)
                throw new ArgumentNullException(nameof(scoreRound));

            if (notifyScoreRound == null)
                throw new ArgumentNullException(nameof(notifyScoreRound));

            this.scoreRound = scoreRound;
            this.notifyScoreRound = notifyScoreRound;
        }

        [HttpPost("score")]
        public async Task<IActionResult> Post(DTOs.ScoreRound request)
        {
            await scoreRound.Score(request);
            var notif = await notifyScoreRound.Notify(request.Round.Id);
            
            return Ok(notif);
        }
    }
}