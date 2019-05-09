namespace KillOrHeal.Domain.Communication.Events.Player
{
    public class JoinFactionEvent: BaseFactionEvent
    {
        public JoinFactionEvent(int playerId, int factionId) : base(playerId, factionId)
        {
        }
    }
}