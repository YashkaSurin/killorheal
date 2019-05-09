using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public class PlayerJoinedFactionEvent: ServerEvent
    {
        public PlayerJoinedFactionEvent(PlayerState player, int factionId)
            : base(string.Format(Constants.Messages.PlayerJoinedFactionTemplate, "Player " + player.PlayerId, "Faction " + factionId))
        {
            Player = player;
            FactionId = factionId;
        }

        public PlayerState Player { get; }
        public int FactionId { get; }
        public override string Type => Constants.ServerEventTypes.PlayerJoinedFaction;
    }
}
