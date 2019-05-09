using System;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public class PlayerHealthChangedEvent: ServerEvent
    {
        protected readonly int ActorId;
        protected readonly double Impact;
        public int PlayerId { get; }
        public double Health { get; }
        public PlayerHealthChangedEvent(int playerId, int actorId, double impact, double health, string message) : base(message)
        {
            ActorId = actorId;
            Impact = impact;
            PlayerId = playerId;
            Health = health;
        }
    }
}