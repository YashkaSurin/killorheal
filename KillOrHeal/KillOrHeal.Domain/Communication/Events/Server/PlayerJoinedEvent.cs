using KillOrHeal.Common;
using KillOrHeal.Data.Entities;

namespace KillOrHeal.Domain.Communication.Events.Server
{
    public class PlayerJoinedEvent: ServerEvent
    {
        public PlayerJoinedEvent(PlayerState player)
            : base(string.Format(Constants.Messages.PlayerJoinedTemplate, "Player " + player.PlayerId))
        {
            Player = player;
        }

        public PlayerState Player { get; }
        public override string Type => Constants.ServerEventTypes.PlayerJoined;
    }
}