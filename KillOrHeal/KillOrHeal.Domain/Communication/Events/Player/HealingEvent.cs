namespace KillOrHeal.Domain.Communication.Events.Player
{
    public class HealingEvent: PlayerActionEvent
    {
        public HealingEvent(int actorId, int targetId) : base(actorId, targetId)
        {
        }
    }
}