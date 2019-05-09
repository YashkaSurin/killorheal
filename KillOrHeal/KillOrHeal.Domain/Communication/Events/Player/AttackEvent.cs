namespace KillOrHeal.Domain.Communication.Events.Player
{
    public class AttackEvent: PlayerActionEvent
    {
        public AttackEvent(int actorId, int targetId) : base(actorId, targetId)
        {
        }
    }
}