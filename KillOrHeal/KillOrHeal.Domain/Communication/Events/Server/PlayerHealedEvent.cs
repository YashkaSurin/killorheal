using KillOrHeal.Common;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public class PlayerHealedEvent: PlayerHealthChangedEvent
    {
        public PlayerHealedEvent(int playerId, int actorId, double impact, double health) 
            : base(playerId, actorId, impact, health, string.Format(Constants.Messages.HealedTemplate, actorId, playerId))
        {
        }

        public override string Type => Constants.ServerEventTypes.PlayerHealed;
    }
}