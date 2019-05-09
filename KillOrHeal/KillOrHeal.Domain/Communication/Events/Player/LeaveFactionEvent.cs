namespace KillOrHeal.Domain.Communication.Events.Player
{
    public class LeaveFactionEvent: BaseFactionEvent
    {
        public LeaveFactionEvent(int playerId, int factionId) : base(playerId, factionId)
        {
        }
    }
}
