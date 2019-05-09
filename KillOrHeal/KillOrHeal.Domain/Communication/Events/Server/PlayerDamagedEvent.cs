using KillOrHeal.Common;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public class PlayerDamagedEvent: PlayerHealthChangedEvent
    {
        public PlayerDamagedEvent(int targetId, int actorId, double impact, double health) 
            : base(targetId, actorId, impact, health, FormatMessage(targetId, actorId, health))
        {
        }

        public override string Type => Constants.ServerEventTypes.PlayerDamaged;

        private static string FormatMessage(int playerId, int actorId, double health)
        {
            return string.Format(Constants.Messages.AttackedTemplate, actorId, playerId)
                   + (health.Equals(0) ? " " + string.Format(Constants.Messages.KilledTemplate, playerId) : "");
        }
    }
}