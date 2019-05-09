using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public class PlayerLeftFactionEvent: ServerEvent
    {
        public PlayerLeftFactionEvent(PlayerState player, int factionId)
            : base(string.Format(Constants.Messages.PlayerLeftFactionTemplate, "Player " + player.PlayerId, "Faction " + factionId))
        {
            Player = player;
            FactionId = factionId;
        }

        public PlayerState Player { get; }
        public int FactionId { get; }
        public override string Type => Constants.ServerEventTypes.PlayerLeftFaction;
    }
}
