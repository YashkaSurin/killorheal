using KillOrHeal.Domain.Communication;
using KillOrHeal.Domain.Communication.Events.Player;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KillOrHeal.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/factions")]
    public class FactionsController : Controller
    {
        private readonly IEventQueue<PlayerEvent> _eventQueue;

        public FactionsController(IEventQueue<PlayerEvent> eventQueue)
        {
            _eventQueue = eventQueue;
        }

        [HttpPut]
        [Route("{factionId}/join")]
        public void Join([FromRoute] int factionId)
        {
            var selfId = Convert.ToInt32(Request.Headers["authorization"]);
            _eventQueue.Enqueque(new JoinFactionEvent(selfId, factionId));
        }

        [HttpPut]
        [Route("{factionId}/leave")]
        public void Leave([FromRoute] int factionId)
        {
            var selfId = Convert.ToInt32(Request.Headers["authorization"]);
            _eventQueue.Enqueque(new LeaveFactionEvent(selfId, factionId));
        }
    }
}