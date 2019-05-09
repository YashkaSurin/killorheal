using System;
using KillOrHeal.Data.Entities;
using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Player;
using KillOrHeal.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace KillOrHeal.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/players")]
    public class PlayersController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IEventQueue<PlayerEvent> _eventQueue;

        public PlayersController(IGameService gameService, IEventQueue<PlayerEvent> eventQueue)
        {
            _gameService = gameService;
            _eventQueue = eventQueue;
        }

        [HttpPost]
        public PlayerState Join()
        {
            return _gameService.SpawnNewPlayer();
        }

        [HttpPut]
        [Route("{playerId}/attack")]
        public void Attack([FromRoute] int playerId)
        {
            var selfId = Convert.ToInt32(Request.Headers["authorization"]);
            _eventQueue.Enqueque(new AttackEvent(selfId, playerId));;
        }

        [HttpPut]
        [Route("{playerId}/heal")]
        public void Heal([FromRoute] int playerId)
        {
            var selfId = Convert.ToInt32(Request.Headers["authorization"]);
            _eventQueue.Enqueque(new HealingEvent(selfId, playerId));
        }
    }
}