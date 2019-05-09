namespace KillOrHeal.Domain.Communication.Events.Player
{
    public class BaseFactionEvent: PlayerEvent
    {
        public BaseFactionEvent(int playerId, int factionId)
        {
            PlayerId = playerId;
            FactionId = factionId;
        }

        public int PlayerId { get; }
        public int FactionId { get; }
    }
}